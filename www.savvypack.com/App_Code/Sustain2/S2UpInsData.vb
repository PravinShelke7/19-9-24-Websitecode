Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S2GetData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Public Class S2UpInsData
    Public Class UpdateInsert
        Dim Sustain1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
        Dim Sustain2Connection As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
        Dim Econ2Connection As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
#Region "Equipment"
        Public Sub EditEqName(ByVal CaseId As String, ByVal EqId As String, ByVal Des As String)
            Try
                Dim odbUtil As New DBUtil()
                Dim ds As New DataSet
                Dim StrSql As String = ""
                If Des <> "" Then
                    StrSql = "SELECT 1 FROM USERSEQUIPMENT WHERE CASEID=" + CaseId.ToString() + " AND EQUIPID=" + EqId + ""
                    ds = odbUtil.FillDataSet(StrSql, Sustain2Connection)
                    If ds.Tables(0).Rows.Count > 0 Then
                        StrSql = "UPDATE USERSEQUIPMENT SET "
                        StrSql = StrSql + "EQUIPID=" + EqId + ",EQUIPDES='" + Des + "' "
                        StrSql = StrSql + "WHERE CASEID=" + CaseId.ToString() + " AND EQUIPID=" + EqId + " "
                        odbUtil.UpIns(StrSql, Sustain2Connection)
                    Else
                        StrSql = "INSERT INTO USERSEQUIPMENT (CASEID,EQUIPID,EQUIPDES) "
                        StrSql = StrSql + "SELECT " + CaseId + "," + EqId + ",'" + Des + "' FROM DUAL "
                        StrSql = StrSql + "WHERE NOT EXISTS(SELECT 1 FROM USERSEQUIPMENT WHERE CASEID=" + CaseId.ToString() + " AND EQUIPID=" + EqId + ") "
                        odbUtil.UpIns(StrSql, Sustain2Connection)
                    End If
                Else
                    StrSql = "DELETE FROM USERSEQUIPMENT WHERE CASEID=" + CaseId.ToString() + " AND EQUIPID=" + EqId + " "
                    odbUtil.UpIns(StrSql, Sustain2Connection)
                End If



            Catch ex As Exception
                Throw New Exception("S2UpInsData:EditEqName:" + ex.Message.ToString())
            End Try

        End Sub
#End Region
      
#Region "Assumptions"
        Public Sub ExtrusionUpdate(ByVal CaseID As String, ByVal Material() As String, ByVal Qty() As String, ByVal PComp() As String, ByVal SCase() As String, ByVal Weight() As String, ByVal Recyle() As String, ByVal Extra() As String, ByVal Dept() As String, ByVal plate As String, ByVal IsDisUpdate As Boolean)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

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
                StrSqlUpadate = "UPDATE MaterialInput SET"
                For Mat = 0 To 9

                    Dim Matid As New Integer
                    Dim WeightVal As Decimal

                    WeightVal = CDbl(Weight(Mat) / Convwt)
                    Matid = Mat + 1

                    'Material 
                    StrSqlIUpadate = StrSqlIUpadate + " M" + Matid.ToString() + " = " + Material(Mat).ToString() + ","
                    'Quantity
                    StrSqlIUpadate = StrSqlIUpadate + " Q" + Matid.ToString() + " = " + Qty(Mat).ToString() + ","
                    'Sustain case
                    StrSqlIUpadate = StrSqlIUpadate + " C" + Matid.ToString() + " = " + SCase(Mat).ToString() + ","
                    'Weight
                    StrSqlIUpadate = StrSqlIUpadate + " T" + Matid.ToString() + " = " + WeightVal.ToString() + ","
                    'Recycle
                    StrSqlIUpadate = StrSqlIUpadate + " R" + Matid.ToString() + " = " + Recyle(Mat).ToString() + ","
                    'Extra-Process
                    StrSqlIUpadate = StrSqlIUpadate + " E" + Matid.ToString() + " = " + Extra(Mat).ToString() + ","
                    'Dept
                    StrSqlIUpadate = StrSqlIUpadate + " D" + Matid.ToString() + " = " + Dept(Mat).ToString() + ","
                    'Pacage Component
                    StrSqlIUpadate = StrSqlIUpadate + " IP" + Matid.ToString() + " = " + PComp(Mat).ToString() + ","

                Next

                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


                'Updating Printing Plates
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE MATERIALINPUT SET PLATE= " + plate.Replace(",", "").ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2GetData:ExtrusionUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub ProductFormatUpdate(ByVal CaseID As String, ByVal M1 As String, ByVal Input() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable a
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:ProductFormatUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PalletAndTruckUpdate(ByVal CaseID As String, ByVal Pallet() As String, ByVal Truck() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

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

                    If i = 4 Then
                        StrSqlIUpadate = StrSqlIUpadate + " T" + i.ToString() + " = " + (Truck(i) / Convwt).ToString() + ","
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " T" + i.ToString() + " = " + Truck(i).ToString() + ","
                    End If
                Next


                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""

                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:PalletAndTruckUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PalletUpdate(ByVal CaseID As String, ByVal Pallet() As String, ByVal Number() As String, ByVal NoOfUses() As String, ByVal PrefWeight() As String, ByVal PrefErgy() As String, ByVal PrefCO2() As String, ByVal PrefRECOV() As String, ByVal PrefSUSM() As String, ByVal PrefPCREC() As String, ByVal PrefSHIPD() As String, ByVal Dept() As String, ByVal Water() As String, ByVal tabNum As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)
                Dim i As Integer
                If tabNum = "1" Then
                    'Getting Values From Database assign to variable 
                    Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                    Dim Convthick3 As String = dts.Tables(0).Rows(0).Item("convthick3")
                    Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                    Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                    Dim ConvGallon As String = dts.Tables(0).Rows(0).Item("ConvGallon")

                    Dim StrSqlUpadate As String = ""
                    Dim StrSqlIUpadate As String = ""
                    StrSqlUpadate = "UPDATE PalletIN SET"
                    For i = 0 To 9

                        Dim Palid As New Integer
                        Dim WeightPreffered As Decimal
                        Dim SDPreffered As Decimal
                        WeightPreffered = CDbl(PrefWeight(i) / Convwt)
                        SDPreffered = CDbl(PrefSHIPD(i) / Convthick3)
                        Palid = i + 1

                        'Pallet Item 
                        StrSqlIUpadate = StrSqlIUpadate + " M" + Palid.ToString() + " = " + Pallet(i).ToString() + ","
                        'Number
                        StrSqlIUpadate = StrSqlIUpadate + " T" + Palid.ToString() + " = " + Number(i).ToString() + ","
                        'Number Of uses
                        StrSqlIUpadate = StrSqlIUpadate + " R" + Palid.ToString() + " = " + NoOfUses(i).ToString() + ","
                        'Preffered Weight
                        StrSqlIUpadate = StrSqlIUpadate + " W" + Palid.ToString() + " = " + WeightPreffered.ToString() + ","
                        'Preffered CO2
                        StrSqlIUpadate = StrSqlIUpadate + " P" + Palid.ToString() + " = " + PrefCO2(i).ToString() + ","
                        'Preffered Ship distance
                        StrSqlIUpadate = StrSqlIUpadate + " SD" + Palid.ToString() + " = " + SDPreffered.ToString() + ","
                        'Dept
                        StrSqlIUpadate = StrSqlIUpadate + " D" + Palid.ToString() + " = " + Dept(i).ToString() + ","

                    Next

                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                    'Updating  Energy
                    StrSqlUpadate = ""
                    StrSqlIUpadate = ""
                    StrSqlUpadate = "UPDATE PalletEnergyPref SET"
                    For i = 0 To 9
                        Dim Matid As New Integer
                        Dim actualEnergyVal As Decimal
                        actualEnergyVal = CDbl(PrefErgy(i) * Convwt)
                        Matid = i + 1
                        StrSqlIUpadate = StrSqlIUpadate + " M" + Matid.ToString() + " = " + actualEnergyVal.ToString() + ","
                    Next
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                    'Updating  Water
                    StrSqlUpadate = ""
                    StrSqlIUpadate = ""
                    StrSqlUpadate = "UPDATE PALLETWATERPREF SET"
                    For i = 0 To 9
                        Dim actualWaterVal As Decimal
                        actualWaterVal = CDbl(Water(i) * Convwt / ConvGallon)
                        Dim Matid As New Integer
                        Matid = i + 1
                        StrSqlIUpadate = StrSqlIUpadate + " M" + Matid.ToString() + " = " + actualWaterVal.ToString() + ","
                    Next
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)
                ElseIf tabNum = "2" Then
                    Dim StrSqlUpadate As String = ""
                    Dim StrSqlIUpadate As String = ""
                    Dim Palid As New Integer
                    StrSqlUpadate = "UPDATE PalletIN SET"
                    For i = 0 To 9
                        Palid = i + 1
                        'Preffered Preffered Recovery
                        StrSqlIUpadate = StrSqlIUpadate + " REC" + Palid.ToString() + " = " + PrefRECOV(i).ToString() + ","
                        'Preffered SUS Material
                        StrSqlIUpadate = StrSqlIUpadate + " OSH" + Palid.ToString() + " ='" + PrefSUSM(i).ToString() + "' ,"
                        'Preffered PC Recycle
                        StrSqlIUpadate = StrSqlIUpadate + " POC" + Palid.ToString() + " = " + PrefPCREC(i).ToString() + ","
                    Next
                    StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                    StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                    StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                    odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)
                End If


            Catch ex As Exception
                Throw New Exception("S2UpdateData:PalletUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PlantConfigUpdate(ByVal CaseID As String, ByVal DEPTA() As String, ByVal DEPTB() As String, ByVal DEPTC() As String, ByVal DEPTD() As String, ByVal DEPTE() As String, ByVal DEPTF() As String, ByVal DEPTG() As String, ByVal DEPTH() As String, ByVal DEPTI() As String, ByVal DEPTJ() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()


                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE PlantCONFIG SET"
                For i = 0 To 9

                    Dim DeptId As New Integer
                    DeptId = i + 1


                    StrSqlIUpadate = StrSqlIUpadate + " M" + DeptId.ToString() + " = " + DEPTA(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " T" + DeptId.ToString() + " = " + DEPTB(i).ToString() + ","

                    StrSqlIUpadate = StrSqlIUpadate + " S" + DeptId.ToString() + " = " + DEPTC(i).ToString() + ","

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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:PlantConfigUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub EfficiencyUpdate(ByVal CaseID As String, ByVal MATA() As String, ByVal MATB() As String, ByVal MATC() As String, ByVal MATD() As String, ByVal MATE() As String, ByVal MATF() As String, ByVal MATG() As String, ByVal MATH() As String, ByVal MATI() As String, ByVal MATJ() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()


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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:EfficiencyUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub EquipmentUpdate(ByVal CaseID As String, ByVal hidAssetId() As String, ByVal PARP() As String, ByVal ECP() As String, ByVal NGCP() As String, ByVal WATERP() As String, ByVal hidAssetDep() As String, ByVal ASSETNUM() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim ConvGallon As String = dts.Tables(0).Rows(0).Item("CONVGALLON")
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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


                'UPDATE THE EQUIPMENT NUMBERS
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE EQUIPMENTNUMBER SET"
                For i = 0 To 29
                    StrSqlIUpadate = StrSqlIUpadate + " M" + (i + 1).ToString() + " = " + ASSETNUM(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


                'Updating Equipment Plant Area Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim PlanAreaPref As New Integer
                StrSqlUpadate = "UPDATE EQUIPMENTAREA SET"
                For i = 0 To 29
                    Dim planArPreffered As Decimal
                    planArPreffered = CDbl(PARP(i) / CONVAREA2)
                    PlanAreaPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + PlanAreaPref.ToString() + " = " + planArPreffered.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Updating Electricity consumption Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim ECPref As New Integer
                StrSqlUpadate = "UPDATE EQUIPENERGYPREF SET"
                For i = 0 To 29
                    ECPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + ECPref.ToString() + " = " + ECP(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


                'Updating Natural Gas Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim NgPref As New Integer
                StrSqlUpadate = "UPDATE EQUIPNATURALGASPREF SET"
                For i = 0 To 29
                    NgPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + NgPref.ToString() + " = " + NGCP(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Updating Water Consumption Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim WatPref As New Integer
                StrSqlUpadate = "UPDATE EQUIPWATERPREF SET"
                For i = 0 To 29
                    WatPref = i + 1
                    Dim dblWaterCons As Decimal
                    dblWaterCons = CDbl(WATERP(i).Replace(",", "") / ConvGallon)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + WatPref.ToString() + " = " + dblWaterCons.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:EquipmentUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub SupportEquipmentUpdate(ByVal CaseID As String, ByVal hidAssetId() As String, ByVal ECP() As String, ByVal NGCP() As String, ByVal HRS() As String, ByVal WATERP() As String, ByVal hidAssetDep() As String, ByVal ASSETNUM() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim ConvGallon As String = dts.Tables(0).Rows(0).Item("CONVGALLON")
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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Updating Equipment Asset Number Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                'Dim AssetPref As New Integer
                StrSqlUpadate = "UPDATE EQUIPMENT2NUMBER SET"
                For i = 0 To 29
                    StrSqlIUpadate = StrSqlIUpadate + " M" + (i + 1).ToString() + " = " + ASSETNUM(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


                'Updating Electricity consumption Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim ECPref As New Integer
                StrSqlUpadate = "UPDATE EQUIP2ENERGYPREF SET"
                For i = 0 To 29
                    ECPref = i + 1
                    StrSqlIUpadate = StrSqlIUpadate + " M" + ECPref.ToString() + " = " + ECP(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Updating Water Consumption Preffered
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                Dim WatPref As New Integer
                StrSqlUpadate = "UPDATE EQUIP2WATERPREF SET"
                For i = 0 To 29
                    WatPref = i + 1
                    Dim dblWaterCons As Decimal
                    dblWaterCons = CDbl(WATERP(i).Replace(",", "") / ConvGallon)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + WatPref.ToString() + " = " + dblWaterCons.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:SupportEquipmentUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub OperationInUpdate(ByVal CaseID As String, ByVal OMAXRH() As String, ByVal OPINSTR() As String, ByVal DT() As String, ByVal OPWASTE() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Updating Production waste
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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


            Catch ex As Exception
                Throw New Exception("S2UpdateData:OprationInUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PersonnelInUpdate(ByVal CaseID As String, ByVal PosDes() As String, ByVal NoWorker() As String, ByVal DEPTID() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)


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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)



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
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)




            Catch ex As Exception
                Throw New Exception("S2UpdateData:PersonnelInUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PlantConfig2Update(ByVal CaseID As String, ByVal AREA() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

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
                    calVal = CDbl(AREA(i - 2) / CONVAREA2)
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + calVal.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:PlantConfig2Update:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub EnergyUpdate(ByVal CaseID As String, ByVal ERGPPREF() As String, ByVal CFACTORPREF() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim ConvGallon As String = dts.Tables(0).Rows(0).Item("ConvGallon")
                Dim UNITS As String = dts.Tables(0).Rows(0).Item("units")
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE plantENERGY SET"
                For i = 1 To 2

                    Dim MatVal As New Integer
                    MatVal = i + 1
                    If i = 1 Then
                        StrSqlIUpadate = StrSqlIUpadate + " ELECPRICE" + " = " + ERGPPREF(i - 1) + ","
                    ElseIf i = 2 Then
                        StrSqlIUpadate = StrSqlIUpadate + " NGASPRICE" + " = " + ERGPPREF(i - 1) + ","
                    End If
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                '-----------------------------Conversion Factor Preffered--------------------------
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                Dim calVal As Double
                calVal = CDbl(CFACTORPREF(0) / Convwt)
                StrSqlUpadate = "UPDATE PLANTCO2 SET P1= " + calVal.ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)
                '-----------------------------------------------------------------------------------   

                '-----------------------------Conversion Factor Water Preffered--------------------------
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                calVal = CDbl(CFACTORPREF(1) / ConvGallon)
                StrSqlUpadate = "UPDATE PLANTWATER SET P1= " + calVal.ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)
                '-----------------------------------------------------------------------------

            Catch ex As Exception
                Throw New Exception("S2UpdateData:EnergyUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub AdditionalEnergyUpdate(ByVal CaseID As String, ByVal EnergyConsum1() As String, ByVal EnergyConsum2() As String, ByVal EnergyConsum3() As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVGALLON As String = dts.Tables(0).Rows(0).Item("CONVGALLON")
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
               

                For i = 0 To 3
                    Dim calVal As Double
                    calVal = CDbl(EnergyConsum3(i) / CONVGALLON * CONVAREA2)
                    Dim MatVal As New Integer
                    MatVal = i + 13
                    StrSqlIUpadate = StrSqlIUpadate + " P" + MatVal.ToString() + " = " + calVal.ToString() + ","

                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)



            Catch ex As Exception
                Throw New Exception("S2UpdateData:AdditionalEnergyUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub TransportUpdate(ByVal CaseID As String, ByVal SD As String, ByVal TruckEffPre As String, ByVal RailEffPre As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convthick3 As String = dts.Tables(0).Rows(0).Item("convthick3")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")
                Dim CONVAREA2 As String = dts.Tables(0).Rows(0).Item("CONVAREA2")
                Dim UNITS As String = dts.Tables(0).Rows(0).Item("units")
                Dim convgallon As String = dts.Tables(0).Rows(0).Item("convgallon")
                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                Dim calValPref As Double

                'Updating Ship Distance
                calValPref = CDbl(SD / Convthick3)
                StrSqlUpadate = "UPDATE CUSTOMERIN SET M2=" + calValPref.ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                '-----------------------------Truck Fuel Efficiency Preffered--------------------------
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                Dim calVal As Double
                calVal = CDbl(TruckEffPre / Convthick3 * convgallon)
                StrSqlUpadate = "UPDATE CUSTOMERIN SET M3= " + calVal.ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)
                '-----------------------------------------------------------------------------

                '-----------------------------Rail Car Fuel Efficiency Preffered -------------------------
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                calVal = CDbl(RailEffPre / Convthick3 * convgallon)
                StrSqlUpadate = "UPDATE CUSTOMERIN SET M4= " + calVal.ToString()
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)
                '-----------------------------------------------------------------------------



            Catch ex As Exception
                Throw New Exception("S2UpdateData:TransportUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub ExtraProcessUpdate(ByVal CaseID As String, ByVal extraProcess() As String, ByVal Preffered() As String, ByVal Dep() As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable a
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("Convwt")
                Dim Unit As String = dts.Tables(0).Rows(0).Item("Units")
                Dim Convgallon As String = dts.Tables(0).Rows(0).Item("CONVGALLON")
                Dim i As New Integer

                'Updating extra process value
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE FIXEDCOSTPCT SET"

                For i = 0 To 2

                    Dim ExtraProcessVal As Double
                    If i < 2 Then
                        ExtraProcessVal = CDbl(extraProcess(i) / Convwt)
                    Else
                        ExtraProcessVal = CDbl(extraProcess(i) / Convgallon)
                    End If
                    StrSqlIUpadate = StrSqlIUpadate + " M" + (i + 1).ToString() + " = " + ExtraProcessVal.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Updating Preffered value
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE FIXEDCOSTPRE SET"

                For i = 0 To 2

                    Dim PrefferedVal As Double
                    If i < 2 Then
                        PrefferedVal = CDbl(Preffered(i) / Convwt)
                    Else
                        PrefferedVal = CDbl(Preffered(i) / Convgallon)
                    End If
                    StrSqlIUpadate = StrSqlIUpadate + " M" + (i + 1).ToString() + " = " + PrefferedVal.ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Updating Department value
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE FIXEDCOSTDEP SET"

                For i = 0 To 2
                    StrSqlIUpadate = StrSqlIUpadate + " M" + (i + 1).ToString() + " = " + Dep(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:ExtraProcessUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub PrefrencesUpdate(ByVal CaseID As String, ByVal LCI As String, ByVal Units As String, ByVal Effdate As String, ByVal Volume As String, ByVal ErgyCalc As String, ByVal Type As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                StrSql = "UPDATE PREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "INVENTORYTYPE =" + LCI + ", "
                StrSql = StrSql + "UNITS =" + Units + ", "
                StrSql = StrSql + "ERGYCALC ='" + ErgyCalc + "', "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY'), "
                StrSql = StrSql + "ERGYTYPE=" + Type + " "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, Sustain2Connection)


                StrSql = "UPDATE PALLETIN  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "EFFDATE = TO_DATE('" + Effdate + "','MON DD,YYYY'), "
                StrSql = StrSql + "INVENTORYTYPE =" + LCI + " "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, Sustain2Connection)

                StrSql = "UPDATE RESULTSPL  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "PVOLUSE =" + Volume + " "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, Sustain2Connection)


                PrefrencesCalc(CaseID, Units)
            Catch ex As Exception
                Throw New Exception("S1GetData:PrefrencesUpdate:" + ex.Message.ToString())
            End Try

        End Sub

        Protected Sub PrefrencesCalc(ByVal CaseID As String, ByVal Units As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim dsConv As New DataSet()
            Dim ObjGetData As New S2GetData.Selectdata()

            Dim Title1 As String = String.Empty
            Dim Title3 As String = String.Empty
            Dim Title4 As String = String.Empty
            Dim Title7 As String = String.Empty
            Dim Title8 As String = String.Empty
            Dim Title9 As String = String.Empty
            Dim Title10 As String = String.Empty

            Dim Convwt As New Decimal
            Dim Convarea As New Decimal
            Dim Convarea2 As New Decimal
            Dim Convthick As New Decimal
            Dim Convthick2 As New Decimal
            Dim Convthick3 As New Decimal
            Dim Convgallon As New Decimal

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

                    'Conversion
                    Convwt = 1
                    Convarea = 1
                    Convarea2 = 1
                    Convthick = 1
                    Convthick2 = 1
                    Convthick3 = 1
                    Convgallon = 1


                Else

                    'Titles
                    Title1 = "micron"
                    Title3 = "m2"
                    Title4 = "kilometers"
                    Title7 = "sq m"
                    Title8 = "kg"
                    Title9 = "mm"
                    Title10 = "liter"

                    'Conversion
                    Convwt = CDbl(dsConv.Tables(0).Rows(0).Item("KGPLB").ToString())
                    Convarea = CDbl(dsConv.Tables(0).Rows(0).Item("M2PMSI").ToString())
                    Convarea2 = CDbl(dsConv.Tables(0).Rows(0).Item("M2PSQFT").ToString())
                    Convthick = CDbl(dsConv.Tables(0).Rows(0).Item("MICPMIL").ToString())
                    Convthick2 = CDbl(dsConv.Tables(0).Rows(0).Item("MPFT").ToString())
                    Convthick3 = CDbl(dsConv.Tables(0).Rows(0).Item("KMPMILE").ToString())
                    Convgallon = CDbl(dsConv.Tables(0).Rows(0).Item("LITPGAL").ToString())

                End If

                StrSql = "UPDATE PREFERENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "CONVWT=" + Convwt.ToString() + ", "
                StrSql = StrSql + "CONVAREA=" + Convarea.ToString() + ", "
                StrSql = StrSql + "CONVAREA2=" + Convarea2.ToString() + ", "
                StrSql = StrSql + "CONVTHICK=" + Convthick.ToString() + ", "
                StrSql = StrSql + "CONVTHICK2=" + Convthick2.ToString() + ", "
                StrSql = StrSql + "CONVTHICK3=" + Convthick3.ToString() + ", "
                StrSql = StrSql + "CONVGALLON=" + Convgallon.ToString() + ", "
                StrSql = StrSql + "TITLE1='" + Title1 + "', "
                StrSql = StrSql + "TITLE3='" + Title3 + "', "
                StrSql = StrSql + "TITLE4='" + Title4 + "', "
                StrSql = StrSql + "TITLE7='" + Title7 + "', "
                StrSql = StrSql + "TITLE8='" + Title8 + "', "
                StrSql = StrSql + "TITLE9='" + Title9 + "', "
                StrSql = StrSql + "TITLE10='" + Title10 + "' "
                StrSql = StrSql + "WHERE CASEID =  " + CaseID + ""
                odbUtil.UpIns(StrSql, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S1GetData:PrefrencesCalc:" + ex.Message.ToString())
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
                odbUtil.UpIns(StrSql, Sustain2Connection)

                StrSql = "UPDATE NOTES  "
                StrSql = StrSql + "SET NOTE = '" + Notes + "' "
                StrSql = StrSql + "WHERE CASEID=  " + CaseId + "  "
                StrSql = StrSql + "AND ASSUMPTIONTYPE='" + AssumptionCode + "' "
                odbUtil.UpIns(StrSql, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpInsData:NotesUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Intermediate"
        Public Sub EndOfLifeUpdate(ByVal CaseID As String, ByVal MALRE() As String, ByVal MALIN() As String, ByVal MALCM() As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable a
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("Convwt")
                Dim Unit As String = dts.Tables(0).Rows(0).Item("Units")
                Dim i As New Integer

                'Updating Material Recycle value
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE MATENDOFLIFEIN SET"

                For i = 0 To 8
                    If MALRE(i) Is Nothing Then
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " MR" + (i + 1).ToString() + " = " + MALRE(i).ToString() + ","
                    End If
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Updating Material Incineration value
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                StrSqlUpadate = "UPDATE MATENDOFLIFEIN SET"
                For i = 0 To 8
                    If MALIN(i) Is Nothing Then
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " MI" + (i + 1).ToString() + " = " + MALIN(i).ToString() + ","
                    End If
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Updating Material Composting value
                StrSqlUpadate = String.Empty
                StrSqlIUpadate = String.Empty
                StrSqlUpadate = "UPDATE MATENDOFLIFEIN SET"
                For i = 0 To 8
                    If MALCM(i) Is Nothing Then
                    Else
                        StrSqlIUpadate = StrSqlIUpadate + " MC" + (i + 1).ToString() + " = " + MALCM(i).ToString() + ","
                    End If
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:EndOfLifeUpdate:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub ScoreCardUpdate(ByVal CaseID As String, ByVal M10 As String, ByVal M11 As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable a
                Dim Convthick As String = dts.Tables(0).Rows(0).Item("convthick")
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("Convwt")
                Dim Unit As String = dts.Tables(0).Rows(0).Item("Units")
                Dim i As New Integer

                'Updating ScoreCard value
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE SCORECARDINTM SET"
                StrSqlIUpadate = StrSqlIUpadate + " M10 = " + M10.ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate + " M11 = " + M11.ToString() + ","
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:ScoreCardUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Results"
        Public Sub ScoreCardResUpdate(ByVal CaseID As String, ByVal LowRange() As String, ByVal HighRange() As String, ByVal Weightings() As String, ByVal RawStore() As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()
                Dim StrSqlUpadate As String = String.Empty
                Dim StrSqlIUpadate As String = String.Empty
                Dim i As New Integer


                'Low Range And High Range
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE RANGESPREF SET"
                For i = 1 To 9
                    StrSqlIUpadate = StrSqlIUpadate + " LP" + i.ToString() + " = " + LowRange(i).ToString() + ","
                    StrSqlIUpadate = StrSqlIUpadate + " HP" + i.ToString() + " = " + HighRange(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Weightings
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE WEIGHTINGSPREF SET"
                For i = 1 To 9
                    StrSqlIUpadate = StrSqlIUpadate + " WP" + i.ToString() + " = " + Weightings(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

                'Raw Store
                StrSqlUpadate = ""
                StrSqlIUpadate = ""
                StrSqlUpadate = "UPDATE RAWSCORE SET"
                For i = 8 To 9
                    StrSqlIUpadate = StrSqlIUpadate + " M" + i.ToString() + " = " + RawStore(i).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:ScoreCardUpdate:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub CSaleVolumeUpdate(ByVal CaseID As String, ByVal CSalesVolume As String, ByVal CSalesUnit As String)
            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New S2GetData.Selectdata()

                'Preferences
                Dim dts As New DataSet()
                dts = ObjGetData.GetPref(CaseID)

                'Getting Values From Database assign to variable 
                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")

                Dim i As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""

                '-----------------------------Customer Sales Volume Update--------------------------
                If CInt(CSalesUnit) = 0 Then
                    StrSqlUpadate = "UPDATE RESULTSPL SET CUSSALESVOLUME= " + (CDbl(CSalesVolume) / CDbl(Convwt)).ToString()
                Else
                    StrSqlUpadate = "UPDATE RESULTSPL SET CUSSALESVOLUME= " + CDbl(CSalesVolume).ToString()
                End If
                StrSqlUpadate = StrSqlUpadate + " , CUSSALESUNIT= " + CSalesUnit
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)
                '-----------------------------------------------------------------------------------               

            Catch ex As Exception
                Throw New Exception("S2UpdateData:SaleVolumeUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Wizard"
        Public Function UpdateWizard(ByVal CaseId As String) As Integer
            Dim WSessionID As New Integer
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New S2GetData.Selectdata()
            Dim ds As New DataSet()
            Try
                ds = ObjGetData.GetSessionWizardId()
                WSessionID = CInt(ds.Tables(0).Rows(0).Item("SessionId").ToString())
                Dim StrSqlUpadate As String = "INSERT INTO SESSIONVALUES SELECT " + WSessionID.ToString() + "," + CaseId.ToString() + " FROM DUAL"
                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)
            Catch ex As Exception
                Throw (New Exception("E1GetData:UpdateWizard:" + ex.Message.ToString()))
            End Try
            Return WSessionID
        End Function
#End Region

#Region "Case Details"

        Public Sub CaseDesUpdate(ByVal CaseId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String)
            Try


                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New E1GetData.Selectdata()


                Dim StrSqlUpadate As String = ""
                StrSqlUpadate = "UPDATE PERMISSIONSCASES SET CASEDE1 ='" + CaseDe1 + "', CASEDE2 ='" + CaseDe2 + "' WHERE CASEID=" + CaseId.ToString() + " "

                odbUtil.UpIns(StrSqlUpadate, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2UpdateData:ExtrusionUpdate:" + ex.Message.ToString())
            End Try

        End Sub

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
                odbUtil.UpIns(StrSql, Sustain2Connection)

            Catch ex As Exception
                Throw New Exception("S2GetData:ServerDateUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region
#Region "Grouping"
        Public Sub UpdateGroupByCaseID(ByVal OldgrpID As String, ByVal grpID As String, ByVal CaseID As String)
            Dim DtsCount As New DataSet()
            Dim objGetData As New E2GetData.Selectdata()
            Dim seqCount As Integer = 0
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim StrSqlUpadate As String = ""
            Try
                'Deleting Previous Group Entry
                StrSqlUpadate = "DELETE FROM GROUPCASES WHERE GROUPID= " + OldgrpID + " AND CASEID=" + CaseID + " "
                odButil.UpIns(StrSqlUpadate, Econ2Connection)


                'Updating Old Group Server Datae
                StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + OldgrpID + " "
                odButil.UpIns(StrSqlUpadate, Econ2Connection)

                'Updating New Group Server Datae
                StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + grpID + " "
                odButil.UpIns(StrSqlUpadate, Econ2Connection)

                DtsCount = objGetData.GetMaxSEQGCASE(grpID)
                If DtsCount.Tables(0).Rows.Count > 0 Then
                    seqCount = DtsCount.Tables(0).Rows(0).Item("MAXCOUNT").ToString()
                End If

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
                odButil.UpIns(strsql, Econ2Connection)
            Catch ex As Exception
                Throw New Exception("S2GetData:UpdateGroupByCaseID:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub AddGroupName(ByVal Des1 As String, ByVal Des2 As String, ByVal USERID As String)
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Dim GROUPID As String = ""
            'Dim i As Integer = 0
            Try
                'Getting GROUPID from Sequence
                strsql = String.Empty
                strsql = "SELECT SEQGROUPID.NEXTVAL AS GROUPID  "
                strsql = strsql + "FROM DUAL "
                Dts = odButil.FillDataSet(strsql, Econ2Connection)
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
                    odButil.UpIns(strsql, Econ2Connection)

                End If

            Catch ex As Exception
                Throw New Exception("S2GetData:AddGroupName:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub EditGroupName(ByVal grpID As String, ByVal Des1 As String, ByVal Des2 As String)
            Try
                Dim odButil As New DBUtil()
                Dim StrSqlUpadate As String = ""

                StrSqlUpadate = "UPDATE GROUPS SET "
                StrSqlUpadate = StrSqlUpadate + " DES1='" + Des1 + "', "
                StrSqlUpadate = StrSqlUpadate + " DES2='" + Des2 + "' "
                StrSqlUpadate = StrSqlUpadate + " WHERE GROUPID= " + grpID.ToString()
                odButil.UpIns(StrSqlUpadate, Econ2Connection)

            Catch ex As Exception

            End Try
        End Sub
        Public Sub UpdateGroupName(ByVal groupName() As String, ByVal groupDes() As String, ByVal GroupID() As String, ByVal count As Integer)
           
            Dim odButil As New DBUtil()
            'Dim strsql As String = String.Empty
            Dim StrSqlUpadate As String = ""
            Dim i As Integer = 0
            Try
                For i = 0 To count - 1
                    If groupName(i) <> "" Then
                        StrSqlUpadate = "UPDATE GROUPS SET "
                        StrSqlUpadate = StrSqlUpadate + " DES1='" + groupName(i).ToString() + "', "
                        StrSqlUpadate = StrSqlUpadate + " DES2='" + groupDes(i).ToString() + "' "
                        StrSqlUpadate = StrSqlUpadate + " WHERE GROUPID= " + GroupID(i).ToString()
                        odButil.UpIns(StrSqlUpadate, Econ2Connection)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception("S2GetData:UpdateGroupName:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub DeleteGroups(ByVal grpId As String)
            Dim odButil As New DBUtil()
            Dim StrSqlUpadate As String = ""

            Try
                StrSqlUpadate = " DELETE FROM GROUPS WHERE GROUPID=" + grpId
                odButil.UpIns(StrSqlUpadate, Econ2Connection)
                StrSqlUpadate = " DELETE FROM GROUPCASES WHERE GROUPID=" + grpId
                odButil.UpIns(StrSqlUpadate, Econ2Connection)
            Catch ex As Exception
                Throw New Exception("E2Update:DeleteGroup:" + ex.Message.ToString())
            End Try
        End Sub
#End Region
#Region "Bemis"
        Public Sub PermissionStatusUpdate(ByVal CaseId As Integer, ByVal UserId As String)
            Try
                'Update Permissioncases Status
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                'StrSql = "Update Permissionscases set STATUSID=1 WHERE CASEID=" + CaseId.ToString() + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' "
                StrSql = "Update Permissionscases set STATUSID=1 WHERE CASEID=" + CaseId.ToString() + " AND USERID=" + UserId.ToString() + " "
                odbUtil.UpIns(StrSql, Sustain2Connection)

                'Update Permissioncases Status
                StrSql = ""
                'StrSql = "INSERT INTO STATUSUPDATE(CASEID,STATUS,DATED,ACTIONBY,COMMENTS,USERNAME) "
                'StrSql = StrSql + " SELECT " + CaseId.ToString() + ",'Submitted for Approval',sysdate,'" + UserName + "','Case " + CaseId.ToString() + "','" + UserName.ToString() + "' FROM DUAL "
                StrSql = "INSERT INTO STATUSUPDATE(CASEID,STATUS,DATED,ACTIONBY,COMMENTS,USERID) "
                StrSql = StrSql + " SELECT " + CaseId.ToString() + ",'Submitted for Approval',sysdate," + UserId.ToString() + ",'Case " + CaseId.ToString() + "'," + UserId.ToString() + " FROM DUAL "

                odbUtil.UpIns(StrSql, Sustain2Connection)
            Catch ex As Exception
                Throw New Exception("S2UpInsData:PermissionStatusUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region
    End Class
End Class
