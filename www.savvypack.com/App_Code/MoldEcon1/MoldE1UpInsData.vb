Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE1GetData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Public Class MoldE1UpInsData
    Public Class UpdateInsert
        Dim MoldE1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")

#Region "Case Details"
        Public Sub CaseDesUpdate(ByVal CaseId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String)
            Try


                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()


                Dim StrSqlUpadate As String = ""
                StrSqlUpadate = "UPDATE PERMISSIONSCASES SET CASEDE1 ='" + CaseDe1 + "', CASEDE2 ='" + CaseDe2 + "' WHERE CASEID=" + CaseId.ToString() + " "

                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("MoldE1GetData:ExtrusionUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub CaseDesUpdateAll(ByVal CaseId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal CaseDe3 As String)
            Try


                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()


                Dim StrSqlUpadate As String = ""
                StrSqlUpadate = "UPDATE PERMISSIONSCASES SET CASEDE1 ='" + CaseDe1 + "', CASEDE2 ='" + CaseDe2 + "', CASEDE3 ='" + CaseDe3 + "'  WHERE CASEID=" + CaseId.ToString() + " "

                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("MoldE1GetData:ExtrusionUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub CaseDelete(ByVal CaseId As String, ByVal UserName As String)
            Try


                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()


                Dim StrSqlUpadate As String = ""
                StrSqlUpadate = "DELETE FROM PERMISSIONSCASES WHERE CASEID=" + CaseId.ToString() + " "
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

                StrSqlUpadate = "INSERT INTO DELETED(CASEID,USERNAME) VALUES (" + CaseId.ToString() + ",'" + UserName.ToString() + "') "
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


            Catch ex As Exception
                Throw New Exception("MoldE1GetData:ExtrusionUpdate:" + ex.Message.ToString())
            End Try

        End Sub


#End Region

#Region "Assumptions"
        Public Sub ExtrusionUpdate(ByVal CaseID As String, ByVal Material() As String, ByVal Thickness() As String, ByVal Price() As String, ByVal Recyle() As String, ByVal Extra() As String, ByVal Sg() As String, ByVal Dept() As String, ByVal discmatyn As String, ByVal plate As String, ByVal MaterialDis() As String, ByVal WeightDis() As String, ByVal PriceDis() As String, ByVal IsDisUpdate As Boolean)
            Try


                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

                'product formatIn
                Dim dtsProd As New DataSet()
                dtsProd = ObjGetData.GetProductFromatIn(CaseID)

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
                    Dim TotalThicKness As Decimal
                    Dim Totalprice As Decimal
                    TotalThicKness = CDbl(Thickness(Mat) / Convthick)
                    Totalprice = CDbl(Price(Mat) * Convwt / Curr)
                    Matid = Mat + 1

                    'Material 
                    StrSqlIUpadate = StrSqlIUpadate + " M" + Matid.ToString() + " = " + Material(Mat).ToString() + ","
                    'Thickness
                    StrSqlIUpadate = StrSqlIUpadate + " T" + Matid.ToString() + " = " + TotalThicKness.ToString() + ","
                    'Price
                    StrSqlIUpadate = StrSqlIUpadate + " S" + Matid.ToString() + " = " + Totalprice.ToString() + ","
                    'Recycle
                    StrSqlIUpadate = StrSqlIUpadate + " R" + Matid.ToString() + " = " + Recyle(Mat).ToString() + ","
                    'Extra-Process
                    StrSqlIUpadate = StrSqlIUpadate + " E" + Matid.ToString() + " = " + Extra(Mat).ToString() + ","
                    'Sg
                    StrSqlIUpadate = StrSqlIUpadate + " SG" + Matid.ToString() + " = " + Sg(Mat).ToString() + ","
                    'Dept
                    StrSqlIUpadate = StrSqlIUpadate + " D" + Matid.ToString() + " = " + Dept(Mat).ToString() + ","

                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

                'Updating P & L Statement value
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE MATERIALINPUT SET DISCMATYN= " + discmatyn.Replace(",", "").ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

                'Updating Printing Plates
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE MATERIALINPUT SET PLATE= " + plate.Replace(",", "").ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)



                'Updating Discrete Materials,Weight and Price
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE MaterialDISIN SET"
                For Mat = 0 To 2

                    Dim Matid As New Integer
                    Dim DWeight As Decimal
                    Dim DPrice As Decimal


                    Matid = Mat + 1
                    DWeight = CDbl(WeightDis(Mat).Replace(",", "") / Convwt)
                    DPrice = CDbl(PriceDis(Mat).Replace(",", "") / Curr)

                    StrSqlIUpadate = StrSqlIUpadate + " DISID" + Matid.ToString() + " = " + MaterialDis(Mat).ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " DISW" + Matid.ToString() + " = " + DWeight.ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " DISP" + Matid.ToString() + " = " + DPrice.ToString() + ","

                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                If IsDisUpdate Then
                    odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)
                End If




            Catch ex As Exception
                Throw New Exception("E1GetData:ExtrusionUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub ProductFormatUpdate(ByVal CaseID As String, ByVal M1 As String, ByVal Input() As String, ByVal PwtPref As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable a
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Unit As String = dts.Tables(0).Rows(0).Item("Units")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("CONVWT")
                Dim i As New Integer
                Dim PWT As Decimal

                PWT = CDbl(PwtPref) / CDbl(Convwt)

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
                StrSqlIUpadate = StrSqlIUpadate + " PWT= " + PWT.ToString() + ","

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("E1GetData:ProductFormatUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PalletAndTruckUpdate(ByVal CaseID As String, ByVal Pallet() As String, ByVal Truck() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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
                    ElseIf i = 4 Then
                        StrSqlIUpadate = StrSqlIUpadate + " T" + i.ToString() + " = " + (Truck(i) / Convwt).ToString() + ","
                    End If
                Next


                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("E1GetData:ProductFormatUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PalletUpdate(ByVal CaseID As String, ByVal Pallet() As String, ByVal Number() As String, ByVal NoOfUses() As String, ByVal PrefWeight() As String, ByVal PrefPrice() As String, ByVal Dept() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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
                    StrSqlIUpadate = StrSqlIUpadate + " T" + Palid.ToString() + " = " + Number(i).ToString().Replace(",", "") + ","
                    'Number Of uses
                    StrSqlIUpadate = StrSqlIUpadate + " R" + Palid.ToString() + " = " + NoOfUses(i).ToString().Replace(",", "") + ","
                    'Preffered Weight
                    StrSqlIUpadate = StrSqlIUpadate + " W" + Palid.ToString() + " = " + WeightPreffered.ToString().Replace(",", "") + ","
                    'Preffered price
                    StrSqlIUpadate = StrSqlIUpadate + " P" + Palid.ToString() + " = " + PRICEPreffered.ToString().Replace(",", "") + ","
                    'Dept
                    StrSqlIUpadate = StrSqlIUpadate + " D" + Palid.ToString() + " = " + Dept(i).ToString() + ","

                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("E1GetData:PalletUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PlantConfigUpdate(ByVal CaseID As String, ByVal DEPTA() As String, ByVal DEPTB() As String, ByVal DEPTC() As String, ByVal DEPTD() As String, ByVal DEPTE() As String, ByVal DEPTF() As String, ByVal DEPTG() As String, ByVal DEPTH() As String, ByVal DEPTI() As String, ByVal DEPTJ() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()


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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception

            End Try

        End Sub

        Public Sub EquipmentUpdate(ByVal CaseID As String, ByVal hidAssetId() As String, ByVal ASSETP() As String, ByVal PARP() As String, ByVal ECP() As String, ByVal NGCP() As String, ByVal hidAssetDep() As String, ByVal AssetNum() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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

                'Updating Equipment Asset Number Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                'Dim AssetPref As New Integer
                StrSqlUpadate = "UPDATE EQUIPMENTNUMBER SET"
                For i = 0 To 29
                    StrSqlIUpadate = StrSqlIUpadate + " M" + (i + 1).ToString() + " = " + AssetNum(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


                'Updating Equipment Asset
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE equipmentTYPE SET"
                For i = 0 To 29
                    AssetId = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + AssetId.ToString() + " = " + hidAssetId(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)



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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("E1GetData:EquipmentUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub SupportEquipmentUpdate(ByVal CaseID As String, ByVal hidAssetId() As String, ByVal ASSETP() As String, ByVal ECP() As String, ByVal NGCP() As String, ByVal HRS() As String, ByVal CostType() As String, ByVal hidAssetDep() As String, ByVal ASSETNUM() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

                'Updating Maximum Annual Hours
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                Dim ihrs As New Integer
                StrSqlUpadate = "UPDATE EQUIPMENT2MAHRS SET"
                For i = 0 To 29
                    ihrs = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + ihrs.ToString() + " = " + HRS(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


                'Updating CostType
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim CCtype As New Integer
                StrSqlUpadate = "UPDATE EQUIPMENT2COSTTYPE SET"
                For i = 0 To 29
                    CCtype = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + CCtype.ToString() + " = " + CostType(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("E1GetData:SupportEquipmentUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub OperationInUpdate(ByVal CaseID As String, ByVal OPWebWidth() As String, ByVal OpExit() As String, ByVal OMAXRH() As String, ByVal OPINSTR() As String, ByVal DT() As String, ByVal OPWASTE() As String, ByVal DWASTE() As String, ByVal EqUnit() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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

                'Updating Web Width
                Dim opwidth As Integer
                StrSqlUpadate = "UPDATE OPwebwidth SET"
                For i = 0 To 29
                    opwidth = i + 1
                    If OPWebWidth(i) <> Nothing Then
                        OPWebWidth(i) = OPWebWidth(i).ToString().Replace(",", "")
                        Dim dblOPWebWidth As Decimal
                        dblOPWebWidth = CDbl(OPWebWidth(i) / Convthick)
                        StrSqlIUpadate = StrSqlIUpadate + " M" + opwidth.ToString() + " = " + dblOPWebWidth.ToString() + ","
                    End If

                Next
                If (StrSqlIUpadate.Length <> 0) Then
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)
                End If

                'Updating OPExits
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim opexits As Integer
                StrSqlUpadate = "UPDATE OPEXITS SET"
                For i = 0 To 29
                    opexits = i + 1
                    If OpExit(i) <> Nothing Then
                        OpExit(i) = OpExit(i).ToString().Replace(",", "")
                        Dim dblOpExit As Decimal
                        'dblOpExit = CDbl(OpExit(i) / Convthick)
                        dblOpExit = CDbl(OpExit(i))
                        StrSqlIUpadate = StrSqlIUpadate + " M" + opexits.ToString() + " = " + dblOpExit.ToString() + ","
                    End If

                Next
                If (StrSqlIUpadate.Length <> 0) Then
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)
                End If


                'Updating Maximum Annual Run Hours
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim maxRunHrs As New Integer
                StrSqlUpadate = "UPDATE OPmaxRUNhrs SET"
                For i = 0 To 29
                    maxRunHrs = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + maxRunHrs.ToString() + " = " + OMAXRH(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


                'Updating Instantaneous Rate 
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim instRate As New Integer
                StrSqlUpadate = "UPDATE OPinstGRSrate SET"
                For i = 0 To 29
                    instRate = i + 1
                    If EqUnit(i) = "fpm" Then
                        Dim dblInstRate As Decimal = CDbl(OPINSTR(i).Replace(",", "") / Convthick2)
                        StrSqlIUpadate = StrSqlIUpadate + " M" + instRate.ToString() + " = " + dblInstRate.ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " M" + instRate.ToString() + " = " + OPINSTR(i).ToString().Replace(",", "") + ","
                    End If

                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


                'Updating Downtime 
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim downT As New Integer
                StrSqlUpadate = "UPDATE OPdowntime SET"
                For i = 0 To 29
                    downT = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + downT.ToString() + " = " + DT(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

                'Updating waste
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim wasteVal As New Integer
                StrSqlUpadate = "UPDATE OPwaste SET"
                For i = 0 To 29
                    wasteVal = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + wasteVal.ToString() + " = " + OPWASTE(i).ToString().Replace(",", "") + ","
                    StrSqlIUpadate = StrSqlIUpadate + " W" + wasteVal.ToString() + " = " + DWASTE(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)




            Catch ex As Exception
                Throw New Exception("E1GetData:OprationInUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PersonnelInUpdate(ByVal CaseID As String, ByVal PosDes() As String, ByVal NoWorker() As String, ByVal PrefSal() As String, ByVal CostType() As String, ByVal DEPTID() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intPosDes.ToString() + " = " + PosDes(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


                'Updating No of workers
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intNoOfWork As New Integer
                StrSqlUpadate = "UPDATE PersonnelNUM SET"
                For i = 0 To 29
                    intNoOfWork = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intNoOfWork.ToString() + " = " + NoWorker(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


                'Updating  Salary Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intSalPref As New Integer
                Dim SalPrefCal As Decimal
                StrSqlUpadate = "UPDATE PersonnelSAL SET"
                For i = 0 To 29
                    intSalPref = i + 1
                    SalPrefCal = CDbl(PrefSal(i).Replace(",", "") / Curr)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intSalPref.ToString() + " = " + SalPrefCal.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

                'Updating Cost Type
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim wasteVal As New Integer
                StrSqlUpadate = "UPDATE PERSONNELVP SET"
                For i = 0 To 29
                    wasteVal = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + wasteVal.ToString() + " = " + CostType(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

                'Updating Department
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intDept As New Integer
                StrSqlUpadate = "UPDATE PersonnelDEP SET"
                For i = 0 To 29
                    intDept = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + intDept.ToString() + " = " + DEPTID(i).ToString().Replace(",", "") + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)




            Catch ex As Exception
                Throw New Exception("E1GetData:PersonnelInUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub FixedCostUpdate(ByVal CaseID As String, ByVal FixedCostVal() As String, ByVal FixedCostPref() As String, ByVal Dept() As String, ByVal depreciate As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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
                        FixCostVal = CDbl(FixedCostVal(i).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FixCostVal.ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " M" + intFixCostVal.ToString() + " = " + FixedCostVal(i).Replace(",", "").ToString() + ","
                    End If
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


                'Updating Fixed Cost Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intCostPre As New Integer
                Dim PrefCostVal As Decimal
                StrSqlUpadate = "UPDATE fixedcostPRE SET"
                For i = 0 To 29
                    PrefCostVal = CDbl(FixedCostPref(i).Replace(",", "") / Curr)
                    intCostPre = i + 1

                    StrSqlIUpadate = StrSqlIUpadate + " M" + intCostPre.ToString() + " = " + PrefCostVal.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)


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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

                'Updating Depreciation
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim wasteVal As New Integer
                StrSqlUpadate = "UPDATE depreciation SET years= " + depreciate.Replace(",", "").ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)




            Catch ex As Exception
                Throw New Exception("E1GetData:FixedCostUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub CustomerInUpdate(ByVal CaseID As String, ByVal PRODPUR As String, ByVal SHIPDIST As String, ByVal MILCOST As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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
                For i = 0 To 2
                    If i = 0 Then
                        itemVal = CDbl(PRODPUR.Replace(",", "") / Curr * Convwt)
                        StrSqlIUpadate = StrSqlIUpadate + " M1" + " = " + itemVal.ToString() + ","
                    ElseIf i = 1 Then
                        itemVal = CDbl(SHIPDIST.Replace(",", "") / convthick3)
                        StrSqlIUpadate = StrSqlIUpadate + " M2" + " = " + itemVal.ToString() + ","
                    ElseIf i = 2 Then
                        itemVal = CDbl(MILCOST.Replace(",", "") / Curr * convthick3)
                        StrSqlIUpadate = StrSqlIUpadate + " M3" + " = " + itemVal.ToString() + ","
                    End If
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("E1GetData:CustomerINUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub EfficiencyUpdate(ByVal CaseID As String, ByVal MATA() As String, ByVal MATB() As String, ByVal MATC() As String, ByVal MATD() As String, ByVal MATE() As String, ByVal MATF() As String, ByVal MATG() As String, ByVal MATH() As String, ByVal MATI() As String, ByVal MATJ() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()


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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("E1GetData:EfficiencyUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PlantConfig2Update(ByVal CaseID As String, ByVal AREA() As String, ByVal PrefLease() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

                'Updating Preffered Cost
                Dim calValPref As Decimal
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE plantSPACE SET"
                For i = 0 To 5
                    'For Production highbay
                    If i = 0 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i).Replace(",", "") / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEHB" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEHB" + " = " + calValPref.ToString() + ","
                        End If
                        'For Production Partial Highbay                       
                    ElseIf i = 1 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i).Replace(",", "") / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEPHB" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASEPHB" + " = " + calValPref.ToString() + ","
                        End If
                        'For Production Standard
                    ElseIf i = 2 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i).Replace(",", "") / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASE" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " PRODUCTIONLEASE" + " = " + calValPref.ToString() + ","
                        End If
                        'For Warehouse
                    ElseIf i = 3 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i).Replace(",", "") / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " WAREHOUSELEASE" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " WAREHOUSELEASE" + " = " + calValPref.ToString() + ","
                        End If
                        'For Office
                    ElseIf i = 4 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i).Replace(",", "") / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " OFFICELEASE" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " OFFICELEASE" + " = " + calValPref.ToString() + ","
                        End If
                        'For Support
                    ElseIf i = 5 Then
                        If UNITS = 0 Then
                            calValPref = CDbl(PrefLease(i).Replace(",", "") / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " SUPPORTLEASE" + " = " + calValPref.ToString() + ","
                        Else
                            calValPref = CDbl(PrefLease(i).Replace(",", "") * CONVAREA2 / Curr)
                            StrSqlIUpadate = StrSqlIUpadate + " SUPPORTLEASE" + " = " + calValPref.ToString() + ","
                        End If

                    End If


                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)







            Catch ex As Exception
                Throw New Exception("E1GetData:EquipmentUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub EnergyUpdate(ByVal CaseID As String, ByVal ERGPPREF() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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
                        calValPref = CDbl(ERGPPREF(i - 1).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " ELECPRICE" + " = " + calValPref.ToString() + ","
                    ElseIf i = 2 Then
                        calValPref = CDbl(ERGPPREF(i - 1).Replace(",", "") / Curr)
                        StrSqlIUpadate = StrSqlIUpadate + " NGASPRICE" + " = " + calValPref.ToString() + ","
                    End If
                Next


                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("E1GetData:EfficiencyUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PrefrencesUpdate(ByVal CaseID As String, ByVal Ocountry As String, ByVal DCountry As String, ByVal Currancy As String, ByVal Units As String, ByVal Effdate As String, ByVal IsDsctNew As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                StrSql = "UPDATE PREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "CURRENCY =" + Currancy + ", "
                StrSql = StrSql + "OCOUNTRY = " + Ocountry + ", "
                StrSql = StrSql + "DCOUNTRY =" + DCountry + ", "
                StrSql = StrSql + "UNITS =" + Units + ", "
                'StrSql = StrSql + "ERGYCALC ='" + ErgyCalc + "', "
                StrSql = StrSql + "ISDSCTNEW ='" + IsDsctNew + "', "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, MoldE1Connection)

                StrSql = "UPDATE MATERIALINPUT  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, MoldE1Connection)

                StrSql = "UPDATE PLANTENERGY  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, MoldE1Connection)


                StrSql = "UPDATE PERSONNELPOS  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY') "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, MoldE1Connection)

                PrefrencesCalc(CaseID, Currancy, Units)
            Catch ex As Exception
                Throw New Exception("E1GetData:PrefrencesUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Protected Sub PrefrencesCalc(ByVal CaseID As String, ByVal Currancy As String, ByVal Units As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim dsConv As New DataSet()
            Dim dsCurr As New DataSet()
            Dim ObjGetData As New MoldE1GetData.Selectdata()

            Dim Title1 As String = String.Empty
            Dim Title2 As String = String.Empty
            Dim Title3 As String = String.Empty
            Dim Title4 As String = String.Empty
            Dim Title6 As String = String.Empty
            Dim Title7 As String = String.Empty
            Dim Title8 As String = String.Empty
            Dim Title9 As String = String.Empty
            'Bug#385
            Dim Title13 As String = String.Empty
            Dim Title14 As String = String.Empty
            Dim Title15 As String = String.Empty
            Dim Title16 As String = String.Empty
            Dim Convvol As New Decimal
            Dim Convwt2 As New Decimal
            Dim Convwt3 As New Decimal
            Dim Convwt4 As New Decimal
            Dim Convarea3 As New Decimal
            'Bug#385

            Dim Convwt As New Decimal
            Dim Convarea As New Decimal
            Dim Convarea2 As New Decimal
            Dim Convthick As New Decimal
            Dim Convthick2 As New Decimal
            Dim Convthick3 As New Decimal
            Dim Curr As New Decimal
            'Bug#441
            Dim Title19 As String = String.Empty
            Dim Title20 As String = String.Empty

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

                    'Bug#385
                    Title13 = "oz"
                    Title14 = "ft3"
                    Title15 = "in2"
                    Title16 = "tons"
                    'Bug#385

                    'Bug#441
                    Title19 = "in2"
                    Title20 = "°F"

                    'Conversion
                    Convwt = 1
                    Convarea = 1
                    Convarea2 = 1
                    Convthick = 1
                    Convthick2 = 1
                    Convthick3 = 1

                    'Bug#385
                    Convvol = 1
                    Convwt2 = 1
                    Convwt3 = 1
                    Convwt4 = 1
                    Convarea3 = 1
                    'Bug#385

                Else

                    'Titles
                    Title1 = "micron"
                    Title3 = "m2"
                    Title4 = "kilometers"
                    Title7 = "sq m"
                    Title8 = "kg"
                    Title9 = "mm"

                    'Bug#385
                    Title13 = "gms"
                    Title14 = "cm3"
                    Title15 = "mm2"
                    Title16 = "KN"
                    'Bug#385

                    'Bug#441
                    Title19 = "m2"
                    Title20 = "°C"

                    'Conversion
                    Convwt = CDbl(dsConv.Tables(0).Rows(0).Item("KGPLB").ToString())
                    Convarea = CDbl(dsConv.Tables(0).Rows(0).Item("M2PMSI").ToString())
                    Convarea2 = CDbl(dsConv.Tables(0).Rows(0).Item("M2PSQFT").ToString())
                    Convthick = CDbl(dsConv.Tables(0).Rows(0).Item("MICPMIL").ToString())
                    Convthick2 = CDbl(dsConv.Tables(0).Rows(0).Item("MPFT").ToString())
                    Convthick3 = CDbl(dsConv.Tables(0).Rows(0).Item("KMPMILE").ToString())

                    'Bug#385
                    Convvol = CDbl(dsConv.Tables(0).Rows(0).Item("CCMPCFT").ToString())
                    Convwt2 = CDbl(dsConv.Tables(0).Rows(0).Item("GMPLB").ToString())
                    Convarea3 = CDbl(dsConv.Tables(0).Rows(0).Item("MM2PIN2").ToString())
                    Convwt3 = CDbl(dsConv.Tables(0).Rows(0).Item("TPKN").ToString())
                    Convwt4 = CDbl(dsConv.Tables(0).Rows(0).Item("GMPOZ").ToString())
                    'Bug#385

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
                'Bug#385
                StrSql = StrSql + "CONVWT2=" + Convwt2.ToString() + ", "
                StrSql = StrSql + "CONVWT3=" + Convwt3.ToString() + ", "
                StrSql = StrSql + "CONVWT4=" + Convwt4.ToString() + ", "
                StrSql = StrSql + "CONVAREA3=" + Convarea3.ToString() + ", "
                StrSql = StrSql + "CONVVOL=" + Convvol.ToString() + ", "
                'Bug#385
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
                StrSql = StrSql + "TITLE9='" + Title9 + "', "
                'Bug#385
                StrSql = StrSql + "TITLE13='" + Title13 + "', "
                StrSql = StrSql + "TITLE14='" + Title14 + "', "
                StrSql = StrSql + "TITLE15='" + Title15 + "', "
                StrSql = StrSql + "TITLE16='" + Title16 + "', "
                'Bug#385
                StrSql = StrSql + "TITLE19='" + Title19 + "', "
                StrSql = StrSql + "TITLE20='" + Title20 + "' "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, MoldE1Connection)
            Catch ex As Exception
                Throw New Exception("E1GetData:PrefrencesUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub AdditionalEnergyUpdate(ByVal CaseID As String, ByVal EnergyConsum1() As String, ByVal EnergyConsum2() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)



            Catch ex As Exception
                Throw New Exception("E1UpdateData:AdditionalEnergyUpdate:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub AdPrefrencesUpdate(ByVal CaseID As String, ByVal ErgyCalc As String, ByVal DFlag As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                StrSql = "UPDATE PREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "ERGYCALC ='" + ErgyCalc + "', "
                StrSql = StrSql + "DFLAG ='" + DFlag + "' "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("E1GetData:AdPrefrencesUpdate:" + ex.Message.ToString())
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
                odbUtil.UpIns(StrSql, MoldE1Connection)

                StrSql = "UPDATE NOTES  "
                StrSql = StrSql + "SET NOTE = '" + Notes + "' "
                StrSql = StrSql + "WHERE CASEID=  " + CaseId + "  "
                StrSql = StrSql + "AND ASSUMPTIONTYPE='" + AssumptionCode + "' "
                odbUtil.UpIns(StrSql, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("MoldE1GetData:NotesUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Results"
        Public Sub ResultsPl(ByVal CaseId As String, ByVal UnitPrice As String, ByVal UnitType As String)
            Try


                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

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
                StrSqlUpadate = "UPDATE RESULTSPL SET UNITPRICE=" + UnitPriceP.ToString() + ", "
                StrSqlUpadate = StrSqlUpadate + "UNITTYPE=" + UnitType.ToString() + " "
                StrSqlUpadate = StrSqlUpadate + "WHERE CASEID = " + CaseId.ToString() + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("MoldE1GetData:ResultsPl:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub CSaleVolumeUpdate(ByVal CaseID As String, ByVal CSalesVolume As String, ByVal CSalesUnit As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                Dim dtSP As New DataSet()
                dts = ObjGetData.GetPref(CaseID)
                dtSP = ObjGetData.GetProfitAndLossDetails(CaseID, False)
                'Getting Values From Database assign to variable 
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim ConvArea As String = dts.Tables(0).Rows(0).Item("CONVAREA")
                Dim StrSqlUpadate As String = ""



                '-----------------------------Customer Sales Volume Update--------------------------
                If CSalesUnit = 0 Then
                    StrSqlUpadate = "UPDATE RESULTSPL SET CUSSALESVOLUME= " + (CDbl(CSalesVolume) / CDbl(Convwt)).ToString()
                Else
                    If dtSP.Tables(0).Rows(0).Item("FINVOLMSI") > 1 Then
                        StrSqlUpadate = "UPDATE RESULTSPL SET CUSSALESVOLUME= " + (CDbl(CSalesVolume) / CDbl(ConvArea)).ToString()
                    ElseIf dtSP.Tables(0).Rows(0).Item("FINVOLMUNITS") > 1 Then
                        StrSqlUpadate = "UPDATE RESULTSPL SET CUSSALESVOLUME= " + CDbl(CSalesVolume).ToString()
                    End If

                End If

                StrSqlUpadate = StrSqlUpadate + ", CUSSALESUNIT = " + CSalesUnit + ""
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)
                '-----------------------------------------------------------------------------------               

            Catch ex As Exception
                Throw New Exception("MoldE1UpdateData:SaleVolumeUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Chart"
        Public Sub UpdateCPreferences(ByVal UserName As String, ByVal Unit As String, ByVal CurId As String, ByVal CurrEffdate As String)

            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
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
                StrSql = StrSql + "FROM ECON.CONVFACTORS "
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)

                If CurId <> 0 Then


                    'For Currecny Conversion
                    StrSql = "SELECT CURRENCYARCH.CURID,  "
                    StrSql = StrSql + "CURRENCYARCH.CURPUSD, "
                    StrSql = StrSql + "CURRENCYARCH.EFFDATE "
                    StrSql = StrSql + "FROM ECON.CURRENCYARCH "
                    StrSql = StrSql + "WHERE CURRENCYARCH.EFFDATE = TO_DATE('" + CurrEffdate + "','MON DD,YYYY') "
                    StrSql = StrSql + "AND CURRENCYARCH.CURID = " + CurId + ""
                    Dts1 = odbUtil.FillDataTable(StrSql, MyConnectionString)
                    Curr = Dts1.Rows(0).Item("CURPUSD").ToString
                Else
                    Curr = "1"
                End If

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
                StrSql = StrSql + "WHERE NOT EXISTS (SELECT 1 FROM CHARTPREFERENCES WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "')"
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
                    StrSql = StrSql + "CURREFFDATE=TO_DATE('" + CurrEffdate + "','MON DD,YYYY'), "
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
                StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper() + "' "
                odbUtil.UpIns(StrSql, MyConnectionString)



            Catch ex As Exception
                Throw (New Exception("MoldE1GetData:UpdateCPreferences:" + ex.Message.ToString()))
            End Try
        End Sub

        Public Sub UpdateCSPreferences(ByVal UserName As String, ByVal Unit As String)

            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
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
                StrSql = StrSql + "FROM ECON.CONVFACTORS "
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)


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


                'INSERTING THE CHARTPREFERENCES
                StrSql = "INSERT INTO CHARTPREFERENCES  "
                StrSql = StrSql + "(USERNAME) "
                StrSql = StrSql + "SELECT '" + UserName + "' FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS (SELECT 1 FROM CHARTPREFERENCES WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "')"
                odbUtil.UpIns(StrSql, MyConnectionString)


                'UPDATING THE CHARTPREFERENCES
                StrSql = "UPDATE CHARTPREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "UNITS=" + Unit + ", "
                StrSql = StrSql + "CONVWT=" + Convwt + ", "
                StrSql = StrSql + "CONVAREA=" + Convarea + ", "
                StrSql = StrSql + "CONVAREA2=" + Convarea2 + ", "
                StrSql = StrSql + "CONVTHICK=" + Convthick + ", "
                StrSql = StrSql + "CONVTHICK2=" + Convthick2 + ", "
                StrSql = StrSql + "CONVTHICK3=" + Convthick3 + ", "
                StrSql = StrSql + "CONVGALLON=" + Convgallon + ", "
                StrSql = StrSql + "TITLE1='" + Title1 + "', "
                StrSql = StrSql + "TITLE3='" + Title3 + "', "
                StrSql = StrSql + "TITLE4='" + Title4 + "', "
                StrSql = StrSql + "TITLE7='" + Title7 + "', "
                StrSql = StrSql + "TITLE8='" + Title8 + "', "
                StrSql = StrSql + "TITLE9='" + Title9 + "', "
                StrSql = StrSql + "TITLE10='" + Title10 + "' "
                StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper() + "' "
                odbUtil.UpIns(StrSql, MyConnectionString)



            Catch ex As Exception
                Throw (New Exception("E1GetData:UpdateCSPreferences:" + ex.Message.ToString()))
            End Try
        End Sub
#End Region

#Region "Wizard"
        Public Function UpdateWizard(ByVal CaseId As String) As Integer
            Dim WSessionID As New Integer
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New MoldE1GetData.Selectdata()
            Dim ds As New DataSet()
            Try
                ds = ObjGetData.GetSessionWizardId()
                WSessionID = CInt(ds.Tables(0).Rows(0).Item("SessionId").ToString())
                Dim StrSqlUpadate As String = "INSERT INTO SESSIONVALUES SELECT " + WSessionID.ToString() + "," + CaseId.ToString() + " FROM DUAL"
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)
            Catch ex As Exception
                Throw (New Exception("MoldE1GetData:UpdateWizard:" + ex.Message.ToString()))
            End Try
            Return WSessionID
        End Function
#End Region

#Region "ServerDate Update"
        Public Sub ServerDateUpdate(ByVal CaseId As Integer, ByVal UserName As String)
            Try


                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                If CaseId < 1000 Then
                    StrSql = "Update BASECASES set ServerDate=SYSDATE WHERE CASEID=" + CaseId.ToString() + " "
                Else
                    'StrSql = "Update Permissionscases set ServerDate=SYSDATE WHERE CASEID=" + CaseId.ToString() + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' "
					StrSql = "Update Permissionscases set ServerDate=SYSDATE WHERE CASEID=" + CaseId.ToString() + " AND USERID=(SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                End If
                odbUtil.UpIns(StrSql, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("MoldE1GetData:ServerDateUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Grouping"
        Public Sub AddGroupName(ByVal Des1 As String, ByVal Des2 As String, ByVal USERID As String)
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
                Dts = odButil.FillDataSet(strsql, MoldE1Connection)
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
                    strsql = strsql + "UPDATEDATE "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT " + GROUPID + ", "
                    strsql = strsql + "'" + Des1 + "', "
                    strsql = strsql + "'" + Des2 + "', "
                    strsql = strsql + USERID + ", "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "sysdate "
                    strsql = strsql + "FROM DUAL "
                    strsql = strsql + "WHERE NOT EXISTS "
                    strsql = strsql + "( "
                    strsql = strsql + "SELECT 1 "
                    strsql = strsql + "FROM "
                    strsql = strsql + "GROUPS "
                    strsql = strsql + "WHERE "
                    strsql = strsql + "USERID=" + USERID + " "
                    strsql = strsql + "AND "
                    strsql = strsql + "DES1='" + Des1 + "' "
                    strsql = strsql + "AND "
                    If Des2 = "" Then
                        strsql = strsql + "DES2 IS NULL "
                    Else
                        strsql = strsql + "DES2='" + Des2 + "' "
                    End If
                    strsql = strsql + "AND SERVICEID IS NULL "

                    strsql = strsql + ") "
                    odButil.UpIns(strsql, MoldE1Connection)

                End If

            Catch ex As Exception
                Throw New Exception("MoldE1UpdateData:AddGroupName:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub UpdateGroupByCaseID(ByVal OldgrpID As String, ByVal grpID As String, ByVal CaseID As String)
            Dim DtsCount As New DataSet()
            Dim objGetData As New MoldE1GetData.Selectdata()
            Dim seqCount As Integer = 0
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim StrSqlUpadate As String = ""
            Try
                'Deleting Previous Group Entry
                StrSqlUpadate = "DELETE FROM GROUPCASES WHERE GROUPID= " + OldgrpID + " AND CASEID=" + CaseID + " "
                odButil.UpIns(StrSqlUpadate, MoldE1Connection)

                'Updating Old Group Server Datae
                StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + OldgrpID + " "
                odButil.UpIns(StrSqlUpadate, MoldE1Connection)

                'Updating New Group Server Datae
                StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + grpID + " "
                odButil.UpIns(StrSqlUpadate, MoldE1Connection)

                

                'Inserting new group Id
                If grpID <> "0" Then
                    DtsCount = objGetData.GetMaxSEQGCASE(grpID)
                    If DtsCount.Tables(0).Rows.Count > 0 Then
                        seqCount = DtsCount.Tables(0).Rows(0).Item("MAXCOUNT").ToString()
                    End If

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
                    odButil.UpIns(strsql, MoldE1Connection)
                End If
            Catch ex As Exception
                Throw New Exception("MoldE1UpdateData:UpdateGroupByCaseID:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub UpdateGroupName(ByVal groupName() As String, ByVal groupDes() As String, ByVal GroupID() As String, ByVal count As Integer)
            Dim DtsCount As New DataSet()
            Dim objGetData As New MoldE1GetData.Selectdata()
            Dim seqCount As Integer = 0
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim StrSqlUpadate As String = ""
            Dim i As Integer = 0
            Try
                For i = 0 To count - 1
                    If groupName(i) <> "" Then
                        StrSqlUpadate = "UPDATE GROUPS SET "
                        StrSqlUpadate = StrSqlUpadate + " DES1='" + groupName(i).ToString() + "', "
                        StrSqlUpadate = StrSqlUpadate + " DES2='" + groupDes(i).ToString() + "' "
                        StrSqlUpadate = StrSqlUpadate + " WHERE GROUPID= " + GroupID(i).ToString()
                        odButil.UpIns(StrSqlUpadate, MoldE1Connection)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception("MoldE1GetData:UpdateGroupName:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub AddGroup(ByVal Des1 As String, ByVal Des2 As String, ByVal USERID As String, ByVal CaseIds() As String)
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
                Dts = odButil.FillDataSet(strsql, MoldE1Connection)
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
                    strsql = strsql + "UPDATEDATE "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT " + GROUPID + ", "
                    strsql = strsql + "'" + Des1 + "', "
                    strsql = strsql + "'" + Des2 + "', "
                    strsql = strsql + USERID + ", "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "sysdate "
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
                    odButil.UpIns(strsql, MoldE1Connection)

                    'Inserting Into GroupsCases
                    For i = 0 To CaseIds.Length - 1
                        If CaseIds(i) <> "" Then
                            strsql = "INSERT INTO GROUPCASES  "
                            strsql = strsql + "( "
                            strsql = strsql + "GROUPCASEID, "
                            strsql = strsql + "GROUPID, "
                            strsql = strsql + "CASEID, "
                            strsql = strsql + "SEQ "
                            strsql = strsql + ") "
                            strsql = strsql + "SELECT SEQGROUPCASEID.NEXTVAL, "
                            strsql = strsql + GROUPID + ", "
                            strsql = strsql + CaseIds(i) + ", "
                            strsql = strsql + (i + 1).ToString() + " "
                            strsql = strsql + "FROM DUAL "
                            strsql = strsql + "WHERE NOT EXISTS "
                            strsql = strsql + "( "
                            strsql = strsql + "SELECT 1 "
                            strsql = strsql + "FROM "
                            strsql = strsql + "GROUPCASES "
                            strsql = strsql + "WHERE "
                            strsql = strsql + "GROUPID=" + GROUPID + " AND "
                            strsql = strsql + "CASEID=" + CaseIds(i) + " "
                            strsql = strsql + ") "
                            odButil.UpIns(strsql, MoldE1Connection)
                        End If
                    Next


                End If

            Catch ex As Exception
                Throw New Exception("MoldE1GetData:AddGroup:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub UpdateGroup(ByVal grpId As String, ByVal Des1 As String, ByVal Des2 As String, ByVal USERID As String, ByVal CaseIds() As String)
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Dim DtsCount As New DataSet()
            Dim objGetData As New MoldE1GetData.Selectdata()
            Dim i As Integer = 0
            Dim seqCount As Integer = 0
            Try
                'Getting GROUPID from Sequence
                strsql = String.Empty

                DtsCount = objGetData.GetMaxSEQGCASE(grpId)
                If DtsCount.Tables(0).Rows.Count > 0 Then
                    seqCount = DtsCount.Tables(0).Rows(0).Item("MAXCOUNT").ToString()
                End If
                'Inserting Into GroupsCases
                For i = 0 To CaseIds.Length - 1
                    If CaseIds(i) <> "" Then
                        strsql = "INSERT INTO GROUPCASES  "
                        strsql = strsql + "( "
                        strsql = strsql + "GROUPCASEID, "
                        strsql = strsql + "GROUPID, "
                        strsql = strsql + "CASEID, "
                        strsql = strsql + "SEQ "
                        strsql = strsql + ") "
                        strsql = strsql + "SELECT SEQGROUPCASEID.NEXTVAL, "
                        strsql = strsql + grpId + ", "
                        strsql = strsql + CaseIds(i) + ", "
                        strsql = strsql + (seqCount + (i + 1)).ToString() + " "
                        strsql = strsql + "FROM DUAL "
                        strsql = strsql + "WHERE NOT EXISTS "
                        strsql = strsql + "( "
                        strsql = strsql + "SELECT 1 "
                        strsql = strsql + "FROM "
                        strsql = strsql + "GROUPCASES "
                        strsql = strsql + "WHERE "
                        strsql = strsql + "GROUPID=" + grpId + " AND "
                        strsql = strsql + "CASEID=" + CaseIds(i) + " "
                        strsql = strsql + ") "
                        odButil.UpIns(strsql, MoldE1Connection)
                    End If
                Next




            Catch ex As Exception
                Throw New Exception("MoldE1GetData:AddGroup:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub DeleteGroup(ByVal grpID As String)
            Try
                Dim odButil As New DBUtil()
                Dim StrSqlUpadate As String = ""
                StrSqlUpadate = "DELETE FROM GROUPCASES WHERE GROUPID= " + grpID + " "
                odButil.UpIns(StrSqlUpadate, MoldE1Connection)
            Catch ex As Exception

            End Try
        End Sub
        Public Sub EditGroupName(ByVal grpID As String, ByVal Des1 As String, ByVal Des2 As String)
            Try
                Dim odButil As New DBUtil()
                Dim i, val As Integer
                val = 1
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE GROUPS SET "
                StrSqlUpadate = StrSqlUpadate + " DES1='" + Des1 + " ', "
                StrSqlUpadate = StrSqlUpadate + " DES2='" + Des2 + " ' "
                StrSqlUpadate = StrSqlUpadate + " WHERE GROUPID= " + grpID.ToString()
                odButil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception

            End Try
        End Sub
        Public Sub DeleteGroups(ByVal grpId As String)
            Dim odButil As New DBUtil()
            Dim StrSqlUpadate As String = ""

            Try
                StrSqlUpadate = " DELETE FROM GROUPS WHERE GROUPID=" + grpId
                odButil.UpIns(StrSqlUpadate, MoldE1Connection)
                StrSqlUpadate = " DELETE FROM GROUPCASES WHERE GROUPID=" + grpId
                odButil.UpIns(StrSqlUpadate, MoldE1Connection)
            Catch ex As Exception
                Throw New Exception("E1Update:DeleteGroup:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "Injection Molding"
        Public Sub InjectionUpdate(ByVal CaseID As String, ByVal Material() As String, ByVal PartWt() As String, ByVal MaxWallThick() As String, ByVal AvgWallThick() As String, ByVal SurfaceArea() As String, ByVal StackMold As String(), ByVal RunSys() As String, ByVal Levels() As String, ByVal CavPrLevels() As String, ByVal ScrewDia() As String, ByVal InjRate() As String, ByVal DryCycleTm() As String, ByVal EjectTmp() As String, ByVal InjectTmp() As String, ByVal MoldTmp() As String, ByVal NoOfPresses() As String, ByVal FixEnrgyLoad() As String, ByVal ProcessLoad() As String, ByVal tabNum As String)

            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)
                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convarea3 As String = dts.Tables(0).Rows(0).Item("convarea3")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                'Dim Convwt2 As String = dts.Tables(0).Rows(0).Item("convwt2")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim ConvVolume As String = dts.Tables(0).Rows(0).Item("Convvol")
                Dim i As Integer
                If tabNum = "1" Then


                    Dim StrSqlUpadate As String = ""
                    Dim StrSqlIUpadate As String = ""
                    StrSqlUpadate = "UPDATE InjectionInput SET"
                    For i = 0 To 9

                        Dim Palid As New Integer
                        Dim PartWeight As Decimal
                        Dim MaxWallThickness As Decimal
                        Dim SA As Decimal
                        PartWeight = CDbl(PartWt(i)) / CDbl(Convwt)
                        MaxWallThickness = CDbl(MaxWallThick(i) / Convthick)
                        SA = CDbl(SurfaceArea(i) / Convarea3)
                        Palid = i + 1

                        'Material Item 
                        StrSqlIUpadate = StrSqlIUpadate + " MATID" + Palid.ToString() + " = " + Material(i).ToString() + ","
                        'PArt Eeight
                        StrSqlIUpadate = StrSqlIUpadate + " PWT" + Palid.ToString() + " = " + PartWeight.ToString() + ","
                        'MAx Thickness
                        StrSqlIUpadate = StrSqlIUpadate + " MWT" + Palid.ToString() + " = " + MaxWallThickness.ToString() + ","
                        'Avg Thickness
                        StrSqlIUpadate = StrSqlIUpadate + " AWT" + Palid.ToString() + " = " + AvgWallThick(i).ToString() + ","
                        'Surface Area
                        StrSqlIUpadate = StrSqlIUpadate + " SA" + Palid.ToString() + " = " + SA.ToString() + ","


                    Next

                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)
                ElseIf tabNum = "2" Then
                    Dim StrSqlUpadate As String = ""
                    Dim StrSqlIUpadate As String = ""
                    Dim Palid As New Integer
                    StrSqlUpadate = "UPDATE InjectionInput SET"
                    For i = 0 To 9
                        Palid = i + 1
                        'Injection Rate
                        'Stack Mold Status
                        StrSqlIUpadate = StrSqlIUpadate + " SMOLD" + Palid.ToString() + " = " + StackMold(i).ToString() + ","
                        'Type of Runer System
                        StrSqlIUpadate = StrSqlIUpadate + " RUNSYS" + Palid.ToString() + " = " + RunSys(i).ToString() + ","
                        'Levels
                        StrSqlIUpadate = StrSqlIUpadate + " LVLS" + Palid.ToString() + " = " + Levels(i).ToString() + ","
                        'Cavitation per Levels
                        StrSqlIUpadate = StrSqlIUpadate + " CAVPL" + Palid.ToString() + " = " + CavPrLevels(i).ToString() + ","
                    Next
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)



                ElseIf tabNum = "3" Then
                    Dim StrSqlUpadate As String = ""
                    Dim StrSqlIUpadate As String = ""
                    Dim Palid As New Integer
                    Dim ScrewD As New Decimal
                    Dim InjRatePS As New Decimal
                    StrSqlUpadate = "UPDATE InjectionInput SET"
                    For i = 0 To 9
                        Palid = i + 1
                        ScrewD = CDbl(ScrewDia(i) / Convthick)
                        InjRatePS = CDbl(InjRate(i) / ConvVolume)
                        'Injection Rate
                        'Screw Diameter
                        StrSqlIUpadate = StrSqlIUpadate + " SD" + Palid.ToString() + " = " + ScrewD.ToString() + ","
                        'Injection Rate
                        StrSqlIUpadate = StrSqlIUpadate + " INJRATEPS" + Palid.ToString() + " = " + InjRatePS.ToString() + ","
                        'Dry cost
                        StrSqlIUpadate = StrSqlIUpadate + " DRYCT" + Palid.ToString() + " = " + DryCycleTm(i).ToString() + ","
                        'Ejection Temp
                        StrSqlIUpadate = StrSqlIUpadate + " EJECTTMP" + Palid.ToString() + " ='" + EjectTmp(i).ToString() + "' ,"
                        'Injection Temp
                        StrSqlIUpadate = StrSqlIUpadate + " INJECTTMP" + Palid.ToString() + " = " + InjectTmp(i).ToString() + ","
                        'Mold Temp
                        StrSqlIUpadate = StrSqlIUpadate + " MOLDTMP" + Palid.ToString() + " = " + MoldTmp(i).ToString() + ","
                    Next
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

                ElseIf tabNum = "5" Then
                    Dim StrSqlUpadate As String = ""
                    Dim StrSqlIUpadate As String = ""
                    Dim Palid As New Integer

                    StrSqlUpadate = "UPDATE InjectionInput SET"
                    For i = 0 To 9
                        Palid = i + 1


                        StrSqlIUpadate = StrSqlIUpadate + " NOPRESS" + Palid.ToString() + " = " + NoOfPresses(i).ToString() + ","

                        StrSqlIUpadate = StrSqlIUpadate + " FIXENERLOAD" + Palid.ToString() + " = " + FixEnrgyLoad(i).ToString() + ","

                        StrSqlIUpadate = StrSqlIUpadate + " PROCESSLOAD" + Palid.ToString() + " = " + ProcessLoad(i).ToString() + ","

                    Next
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)
                End If

            Catch ex As Exception
                Throw New Exception("MoldE1UpdateData:InjectionUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Ink Printing"
        Public Sub ColorPreferenceUpdate(ByVal UserName As String, ByVal WetCost() As String, ByVal PerSol() As String, ByVal SGravity() As String, ByVal CaseId As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()
                Dim Palid As New Integer
                Dim i As New Integer

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseId)

                'Getting Values From Database assign to variable 
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")

                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE INKPREFERENCES SET"
                i = 0
                For i = 0 To 10
                    Dim Totalprice As Decimal
                    Totalprice = CDbl(WetCost(i).ToString().Replace(",", "") * Convwt / Curr)

                    Palid = i + 1
                    'Wet Cost
                    'If CDbl(WetCost(i).ToString()) <> 0.0 Then
                    StrSqlIUpadate = StrSqlIUpadate + " WETPRICE" + Palid.ToString() + " = " + Totalprice.ToString() + ","
                    ' End If

                    '% Solid
                    ' If CDbl(PerSol(i).ToString()) <> 0.0 Then
                    StrSqlIUpadate = StrSqlIUpadate + " PERSOL" + Palid.ToString() + " = " + PerSol(i).ToString().Replace(",", "") + ","
                    ' End If

                    'Specific Gravity
                    '  If CDbl(SGravity(i).ToString()) <> 0.0 Then
                    StrSqlIUpadate = StrSqlIUpadate + " SGRAVITY" + Palid.ToString() + " = " + SGravity(i).ToString().Replace(",", "") + ","
                    ' End If

                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE UPPER(USERNAME) = '" + UserName.ToUpper() + "' "
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("MoldE1UpdateData:ColorPreferenceUpdate:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub ColorInputUpdate(ByVal CaseID As String, ByVal Color() As String, ByVal Coverage() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim Palid As New Integer
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE COLORINPUT SET"
                For i = 0 To 9
                    Palid = i + 1
                    'Color Code
                    StrSqlIUpadate = StrSqlIUpadate + " COLOR" + Palid.ToString() + " = " + Color(i).ToString() + ","
                    'Coverage
                    StrSqlIUpadate = StrSqlIUpadate + " COV" + Palid.ToString() + " = " + Coverage(i).ToString().Replace(",", "") + ","
                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("MoldE1UpdateData:ColorInputUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Bemis"
        Public Sub PermissionStatusUpdate(ByVal CaseId As Integer, ByVal UserName As String)
            Try
                'Update Permissioncases Status
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                StrSql = "Update Permissionscases set STATUSID=1 WHERE CASEID=" + CaseId.ToString() + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' "
                odbUtil.UpIns(StrSql, MoldE1Connection)

                'Update Permissioncases Status
                StrSql = ""
                StrSql = "INSERT INTO STATUSUPDATE(CASEID,STATUS,DATED,ACTIONBY,COMMENTS,USERNAME) "
                StrSql = StrSql + " SELECT " + CaseId.ToString() + ",'Submitted for Approval',sysdate,'" + UserName + "','Case " + CaseId.ToString() + "','" + UserName.ToString() + "' FROM DUAL "
                odbUtil.UpIns(StrSql, MoldE1Connection)
            Catch ex As Exception
                Throw New Exception("MoldE1UpInsData:PermissionStatusUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Barrier"
        Public Sub BarrierUpdateNew(ByVal CaseID As String, ByVal OTRTemp As String, ByVal WVTRTemp As String, ByVal OTRRH As String, ByVal WVTRRH As String, ByRef OTR() As String, ByVal WVTR() As String, ByVal GRADE() As String, ByVal isBarrier As String)
            Try


                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()


                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim Conarea As String = dts.Tables(0).Rows(0).Item("CONVAREA")

                Dim Mat As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                Dim totOTRTTemp As Decimal
                Dim totWVTRTemp As Decimal
                If dts.Tables(0).Rows(0).Item("UNITS").ToString <> 1 Then
                    totOTRTTemp = CDbl(OTRTemp)
                    totWVTRTemp = CDbl(WVTRTemp)
                Else
                    totOTRTTemp = CDbl(OTRTemp * (9 / 5)) + 32
                    totWVTRTemp = CDbl(WVTRTemp * (9 / 5)) + 32
                End If

                If isBarrier = "1" Then
                    StrSqlUpadate = "UPDATE MATERIALINPUT SET"
                    StrSqlIUpadate = StrSqlIUpadate + " OTRTEMP  = " + totOTRTTemp.ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " WVTRTEMP  = " + totWVTRTemp.ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " OTRRH  = " + OTRRH + ","
                    StrSqlIUpadate = StrSqlIUpadate + " WVTRRH  = " + WVTRRH + ","
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)
                End If

                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                StrSqlUpadate = "UPDATE BARRIERINPUT SET"
                For Mat = 0 To 9
                    Dim Matid As New Integer
                    Matid = Mat + 1
                    'Material 
                    If isBarrier = "1" Then
                        If IsNumeric(OTR(Mat)) Then
                            If dts.Tables(0).Rows(0).Item("UNITS").ToString <> 0 Then
                                OTR(Mat) = CDbl(OTR(Mat)) * (CDbl(Conarea) / 1000) * 100
                            End If
                            StrSqlIUpadate = StrSqlIUpadate + " OTR" + Matid.ToString() + " = " + CDbl(OTR(Mat)).ToString() + ","
                        Else
                            StrSqlIUpadate = StrSqlIUpadate + " OTR" + Matid.ToString() + " = null,"
                        End If
                        If IsNumeric(WVTR(Mat)) Then
                            If dts.Tables(0).Rows(0).Item("UNITS").ToString <> 0 Then
                                WVTR(Mat) = CDbl(WVTR(Mat)) * (CDbl(Conarea) / 1000) * 100
                            End If

                            StrSqlIUpadate = StrSqlIUpadate + " WVTR" + Matid.ToString() + " = " + CDbl(WVTR(Mat)).ToString() + ","
                        Else
                            StrSqlIUpadate = StrSqlIUpadate + " WVTR" + Matid.ToString() + " = null,"
                        End If
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
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception
                Throw New Exception("BarrierUpdateNew:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub GradeUpdateNew(ByVal CaseID As Integer)
            Try
                Dim objGetdata As New MoldE1GetData.Selectdata()
                Dim dsMat As New DataSet()
                Dim Mat As Integer = 0
                dsMat = objGetdata.GetExtrusionDetailsBarrD(CaseID)
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                Dim gradeid As Integer
                Dim odbUtil As New DBUtil()

                StrSqlUpadate = "UPDATE BARRIERINPUT SET"
                For Mat = 0 To 9
                    Dim Matid As New Integer
                    Matid = Mat + 1
                    'Grade 
                    If dsMat.Tables(0).Rows(0).Item("M" + Matid.ToString() + "") = 0 Then
                        StrSqlIUpadate = StrSqlIUpadate + " GRADE" + Matid.ToString() + " = 0,"
                    Else
                        gradeid = objGetdata.GetMatGrades(dsMat.Tables(0).Rows(0).Item("M" + Matid.ToString() + ""), dsMat.Tables(0).Rows(0).Item("GRADE" + Matid.ToString() + ""))
                        StrSqlIUpadate = StrSqlIUpadate + " GRADE" + Matid.ToString() + " = " + gradeid.ToString() + ","
                    End If
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID.ToString() + ""
                odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)

            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "Competitive Module"
        Public Sub UpdateServiceId(ByVal CaseId As String, ByVal ServiceIdE1 As String, ByVal ServiceIdS1 As String, ByVal UserName As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Connection As String = String.Empty
            Connection = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
            Dim S1Connection As String = String.Empty
            S1Connection = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
            Dim Dts As New DataSet()
            Try
                'Update on Econ
                StrSql = "UPDATE PERMISSIONSCASES SET SERVICEID=" + ServiceIdE1 + " WHERE CASEID= " + CaseId + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' "
                odbUtil.UpIns(StrSql, MoldE1Connection)

                'Update on Sustain1
                StrSql = "UPDATE PERMISSIONSCASES SET SERVICEID=" + ServiceIdS1 + " WHERE CASEID= " + CaseId + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' "
                odbUtil.UpIns(StrSql, S1Connection)
            Catch ex As Exception
                Throw New Exception("UpdateServiceId:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub AddCompGroupName(ByVal Des1 As String, ByVal Des2 As String, ByVal USERID As String, ByVal ServiceId As String)
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
                Dts = odButil.FillDataSet(strsql, MoldE1Connection)
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
                    strsql = strsql + "SERVICEID "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT " + GROUPID + ", "
                    strsql = strsql + "'" + Des1 + "', "
                    strsql = strsql + "'" + Des2 + "', "
                    strsql = strsql + USERID + ", "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + ServiceId + " "
                    strsql = strsql + "FROM DUAL "
                    strsql = strsql + "WHERE NOT EXISTS "
                    strsql = strsql + "( "
                    strsql = strsql + "SELECT 1 "
                    strsql = strsql + "FROM "
                    strsql = strsql + "GROUPS "
                    strsql = strsql + "WHERE "
                    strsql = strsql + "DES1='" + Des1 + "' "
                    strsql = strsql + "AND "
                    If Des2 = "" Then
                        strsql = strsql + "DES2 IS NULL "
                    Else
                        strsql = strsql + "DES2='" + Des2 + "' "
                    End If
                    strsql = strsql + "AND SERVICEID IS NOT NULL "
                    strsql = strsql + ") "
                    odButil.UpIns(strsql, MoldE1Connection)
                End If

            Catch ex As Exception
                Throw New Exception("MoldE1GetData:AddCompGroupName:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "Depreciation Cost"
        Public Sub DepreciationUpdate(ByVal CaseID As String, ByVal depreciate() As String, ByVal depreciateSE() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New MoldE1GetData.Selectdata()
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")

                'Updating Numner Of Dep For Process Equip
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intDepId As New Integer
                StrSqlUpadate = "UPDATE EQUIPMENTNUMBER SET"
                For i = 0 To 29
                    intDepId = i + 1
                    If depreciate(i) <> "" Then
                        StrSqlIUpadate = StrSqlIUpadate + " D" + intDepId.ToString() + " = " + depreciate(i).ToString() + ","
                    End If
                Next
                If StrSqlIUpadate <> "" Then
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)
                End If


                'Updating Numner Of Dep For Process Equip
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim intDepSEId As New Integer
                StrSqlUpadate = "UPDATE EQUIPMENT2NUMBER SET"
                For i = 0 To 29
                    intDepSEId = i + 1
                    If depreciateSE(i) <> "" Then
                        StrSqlIUpadate = StrSqlIUpadate + " D" + intDepSEId.ToString() + " = " + depreciateSE(i).ToString() + ","
                    End If
                Next
                If StrSqlIUpadate <> "" Then
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, MoldE1Connection)
                End If




            Catch ex As Exception
                Throw New Exception("E1GetData:DepreciationUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

    End Class
End Class
