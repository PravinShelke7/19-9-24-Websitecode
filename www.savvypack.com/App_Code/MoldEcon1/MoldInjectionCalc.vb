Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE1GetData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Public Class MoldInjectionCalc
    Public Class Calculation
        Dim MoldE1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
        Public CoolingTm(10) As String
        Public Mwt(10) As String
        Dim MoldTemp(10) As String
        Dim EjectTemp(10) As String
        Dim InjectTemp(10) As String
        Public EffDiffusivity(10) As String
        Public DwellTm(10) As String
        Public FillTm(10) As String
        Public CycleTm(10) As String
        Public CycleTmPrMin(10) As String
        Public total(19) As String
        Public PWT(10) As String
        Public SA(10) As String
        Public partvol(10) As String
        Public totpsa(10) As String
        Public sarea(10) As String
        Public totsarea(10) As String
        Public screwDia(10) As String
        Public InjRatePS(10) As String
        Public InsThruput(10) As String
        Public InsThruputhr(10) As String
        Public InsThruputkg(10) As String
        Public FixEnerLoad(10) As String
        Public ProcessLoad(10) As String
        Public TotalLoad(10) As String
        Public Benchmark(10) As String
        Public Delta(10) As String
        Public CFE(10) As String
        Public injVol(10) As String
        Dim TempDiff As String


        Public Sub InjectionMoldCalculate(ByVal caseid As String)
            Dim ds As New DataSet

            Dim objGetData As New MoldE1GetData.Selectdata
            For i = 0 To 19
                total(i) = "0"

            Next
            For i = 0 To 10
                FillTm(i) = "0"
                DwellTm(i) = "0"

            Next
            Try
                ds = objGetData.GetInjectionDetails(caseid)
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To 9

                        If CDbl(ds.Tables(0).Rows(0).Item("EjectTmp" + (i + 1).ToString() + "")) = 0.0 Then
                            EjectTemp(i) = ds.Tables(0).Rows(0).Item("EjectTmpp" + (i + 1).ToString() + "")
                        Else
                            EjectTemp(i) = ds.Tables(0).Rows(0).Item("EjectTmp" + (i + 1).ToString() + "")
                        End If
                        If CDbl(ds.Tables(0).Rows(0).Item("InjectTmp" + (i + 1).ToString() + "")) = 0.0 Then
                            InjectTemp(i) = ds.Tables(0).Rows(0).Item("InjectTmpp" + (i + 1).ToString() + "")
                        Else
                            InjectTemp(i) = ds.Tables(0).Rows(0).Item("InjectTmp" + (i + 1).ToString() + "")
                        End If
                        If CDbl(ds.Tables(0).Rows(0).Item("MoldTmp" + (i + 1).ToString() + "")) = 0.0 Then
                            MoldTemp(i) = ds.Tables(0).Rows(0).Item("MoldTmpp" + (i + 1).ToString() + "")
                        Else
                            MoldTemp(i) = ds.Tables(0).Rows(0).Item("MoldTmp" + (i + 1).ToString() + "")
                        End If
                        If CDbl(ds.Tables(0).Rows(0).Item("EFFDIFFUSIVITY" + (i + 1).ToString() + "")) = 0.0 Then
                            EffDiffusivity(i) = 1
                        Else
                            EffDiffusivity(i) = ds.Tables(0).Rows(0).Item("EFFDIFFUSIVITY" + (i + 1).ToString() + "")
                        End If
                        If (ds.Tables(0).Rows(0).Item("pwt" + (i + 1).ToString() + "").ToString() <> "") Then
                            PWT(i) = CDbl(ds.Tables(0).Rows(0).Item("PWT" + (i + 1).ToString() + "").ToString())
                        End If
                        If (ds.Tables(0).Rows(0).Item("MWT" + (i + 1).ToString() + "").ToString() <> "") Then
                            Mwt(i) = CDbl(ds.Tables(0).Rows(0).Item("MWT" + (i + 1).ToString() + "").ToString())
                        End If
                        If (ds.Tables(0).Rows(0).Item("SA" + (i + 1).ToString() + "").ToString() <> "") Then
                            SA(i) = CDbl(ds.Tables(0).Rows(0).Item("SA" + (i + 1).ToString() + "").ToString())
                        End If
                        partvol(i) = CDbl(ds.Tables(0).Rows(0).Item("PARTVOL" + (i + 1).ToString() + "").ToString())
                        totpsa(i) = CDbl(ds.Tables(0).Rows(0).Item("TOTPARTSA" + (i + 1).ToString() + "").ToString())
                        sarea(i) = CDbl(ds.Tables(0).Rows(0).Item("SAREA" + (i + 1).ToString() + "").ToString())
                        totsarea(i) = (CDbl(ds.Tables(0).Rows(0).Item("TOTPARTSA" + (i + 1).ToString() + "").ToString()) + CDbl(ds.Tables(0).Rows(0).Item("SAREA" + (i + 1).ToString() + "").ToString()))
                        If (ds.Tables(0).Rows(0).Item("SD" + (i + 1).ToString() + "").ToString() <> "") Then
                            screwDia(i) = CDbl(ds.Tables(0).Rows(0).Item("SD" + (i + 1).ToString() + "").ToString())
                        End If
                        If (ds.Tables(0).Rows(0).Item("INJRATEPS" + (i + 1).ToString() + "").ToString() <> "") Then
                            InjRatePS(i) = CDbl(ds.Tables(0).Rows(0).Item("INJRATEPS" + (i + 1).ToString() + "").ToString())
                        End If
                        If CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString()) > 0.0 Then
                            injVol(i) = (CDbl(ds.Tables(0).Rows(0).Item("ShotWt" + (i + 1).ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString())).ToString()
                        End If

                        TempDiff = (CDbl(EjectTemp(i)) - CDbl(MoldTemp(i)))
                        If ds.Tables(0).Rows(0).Item("MATID" + (i + 1).ToString() + "") <> 0 And CInt(TempDiff) <> 0 Then
                            CoolingTm(i) = ((CDbl(ds.Tables(0).Rows(0).Item("mwt" + (i + 1).ToString() + "")) * 25.4) * (CDbl(ds.Tables(0).Rows(0).Item("mwt" + (i + 1).ToString() + ""))) * 25.4) / (2 * 3.14 * CDbl(EffDiffusivity(i))) * Math.Log((3.14 / 4) * ((CDbl(InjectTemp(i)) - CDbl(MoldTemp(i))) / CDbl(TempDiff)))
                        Else
                            CoolingTm(i) = "0"
                        End If
                        'Dwell Time

                        'Fill Time
                        If CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString()) > 0.0 And CDbl(ds.Tables(0).Rows(0).Item("INJRATEPS" + (i + 1).ToString() + "").ToString()) > 0.0 Then
                            'FillTm(i) = (((CDbl(ds.Tables(0).Rows(0).Item("ShotWt" + (i + 1).ToString() + "").ToString())) / CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString())) * (CDbl((0.94 * 998.8473) / CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString()))) / (CDbl(ds.Tables(0).Rows(0).Item("INJRATEPS" + (i + 1).ToString() + "").ToString()) * 28317))
                            FillTm(i) = CDbl(injVol(i) * 28317) * (CDbl((0.94 * 998.8473) / CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString()))) / (CDbl(ds.Tables(0).Rows(0).Item("INJRATEPS" + (i + 1).ToString() + "").ToString()) * 28317)
                        Else
                            FillTm(i) = "0"
                        End If
                        If CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString()) > 0.0 And CDbl(ds.Tables(0).Rows(0).Item("INJRATEPS" + (i + 1).ToString() + "").ToString()) > 0.0 Then
                            DwellTm(i) = FillTm(i) * 1
                        Else
                            DwellTm(i) = "0"
                        End If
                        CycleTm(i) = CDbl(CoolingTm(i)) + CDbl(FillTm(i)) + CDbl(DwellTm(i)) + CDbl(ds.Tables(0).Rows(0).Item("DRYCT" + (i + 1).ToString() + "").ToString())
                        If CycleTm(i) <> "0" Then
                            CycleTmPrMin(i) = 60 / CDbl(CycleTm(i))
                        Else
                            CycleTmPrMin(i) = "0"
                        End If
                        'Tab5 Calculation
                        If CycleTmPrMin(i) <> "0" Then
                            InsThruput(i) = CycleTmPrMin(i) * (ds.Tables(0).Rows(0).Item("TOTALCAV" + (i + 1).ToString() + ""))
                        Else
                            InsThruput(i) = "0"
                        End If


                        If InsThruput(i) <> "0" Then
                            InsThruputhr(i) = InsThruput(i) * 60
                        Else
                            InsThruputhr(i) = "0"
                        End If


                        If InsThruputhr(i) <> "0" Then
                            InsThruputkg(i) = (InsThruputhr(i) * ((ds.Tables(0).Rows(0).Item("PWt" + (i + 1).ToString() + "")) * CDbl(ds.Tables(0).Rows(0).Item("CONVwt").ToString())))
                        Else
                            InsThruputkg(i) = "0"
                        End If

                        If ds.Tables(0).Rows(0).Item("FixEnerLoad" + (i + 1).ToString() + "") <> "0" Then
                            'FixEnerLoad(i) = CDbl(ds.Tables(0).Rows(0).Item("FixEnerLoad" + (i + 1).ToString() + "")) / CDbl((ds.Tables(0).Rows(0).Item("Nopress" + (i + 1).ToString() + "")) * (InsThruputhr(i) * ((ds.Tables(0).Rows(0).Item("PWt" + (i + 1).ToString() + "")))))
                            FixEnerLoad(i) = CDbl(ds.Tables(0).Rows(0).Item("FixEnerLoad" + (i + 1).ToString() + "")) / CDbl((ds.Tables(0).Rows(0).Item("Nopress" + (i + 1).ToString() + "")) * (InsThruputkg(i)))
                        Else
                            FixEnerLoad(i) = "0"
                        End If

                        If ds.Tables(0).Rows(0).Item("ProcessLoad" + (i + 1).ToString() + "") <> "0" Then
                            'ProcessLoad(i) = CDbl(ds.Tables(0).Rows(0).Item("ProcessLoad" + (i + 1).ToString() + "")) / (InsThruputhr(i) * ((ds.Tables(0).Rows(0).Item("PWt" + (i + 1).ToString() + ""))))
                            'ProcessLoad(i) = ProcessLoad(i) / (1 / CDbl(ds.Tables(0).Rows(0).Item("CONVwt").ToString()))
                            ProcessLoad(i) = CDbl(ds.Tables(0).Rows(0).Item("ProcessLoad" + (i + 1).ToString() + "")) / (InsThruputkg(i))
                        Else
                            ProcessLoad(i) = "0"
                        End If

                        If FixEnerLoad(i) <> "0" Or ProcessLoad(i) <> "0" Then
                            TotalLoad(i) = CDbl(FixEnerLoad(i)) + CDbl(ProcessLoad(i))
                        Else
                            TotalLoad(i) = "0"
                        End If

                        If InsThruputhr(i) <> "0" Then
                            'Benchmark(i) = 4.4472 * Math.Pow(InsThruputkg(i), -0.3155)
                            If ds.Tables(0).Rows(0).Item("UNITS") <> 1 Then
                                Benchmark(i) = (4.4472 * Math.Pow(InsThruputkg(i), -0.3155)) / 2.205
                            Else
                                Benchmark(i) = 4.4472 * Math.Pow(InsThruputkg(i), -0.3155)
                            End If
                            Benchmark(i) = Benchmark(i)
                        Else
                            Benchmark(i) = "0"
                        End If

                        If Benchmark(i) <> "0" Then
                            Delta(i) = CDbl(TotalLoad(i) - Benchmark(i))
                            Delta(i) = Delta(i)
                        Else
                            Delta(i) = "0"
                        End If
                        CFE(i) = CDbl(CDbl(totsarea(i)) * CDbl(ds.Tables(0).Rows(0).Item("STANDARDWALL" + (i + 1).ToString() + "").ToString()))
                        CFE(i) = CDbl(CFE(i))
                        total(0) = CDbl(total(0)) + partvol(i)
                        total(1) = CDbl(total(1)) + totpsa(i)
                        total(2) = CDbl(total(2)) + sarea(i)
                        total(3) = CDbl(total(3)) + totsarea(i)
                        total(4) = CDbl(total(4)) + CDbl(ds.Tables(0).Rows(0).Item("totalcav" + (i + 1).ToString() + "").ToString())
                        total(5) = CDbl(total(5)) + (CDbl(ds.Tables(0).Rows(0).Item("ShotWt" + (i + 1).ToString() + "").ToString())) ' * CDbl(ds.Tables(0).Rows(0).Item("CONVWT2").ToString()))
                        If CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString()) > 0.0 Then
                            total(6) = CDbl(total(6)) + ((CDbl(ds.Tables(0).Rows(0).Item("ShotWt" + (i + 1).ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString()))) '* CDbl(ds.Tables(0).Rows(0).Item("CONVVOL").ToString()))

                        Else
                            total(6) = CDbl(total(6)) + 0
                        End If

                        If CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString()) > 0.0 Then
                            total(7) = CDbl(total(7)) + (((CDbl(ds.Tables(0).Rows(0).Item("ShotWt" + (i + 1).ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString())) / 0.5) * (CDbl((0.94 * 998.8473) / CDbl(ds.Tables(0).Rows(0).Item("MD" + (i + 1).ToString() + "").ToString())))) ' * CDbl(ds.Tables(0).Rows(0).Item("CONVVOL").ToString()))
                        Else
                            total(7) = CDbl(total(7)) + 0
                        End If

                        total(8) = CDbl(total(8)) + CDbl(FillTm(i))
                        total(9) = CDbl(total(9)) + CDbl(DwellTm(i))
                        total(10) = CDbl(total(10)) + CDbl(CoolingTm(i))
                        total(11) = CDbl(total(11)) + CDbl(CycleTm(i))
                        total(12) = CDbl(total(12)) + CDbl(CycleTmPrMin(i))
                        total(13) = CDbl(total(13)) + CDbl(InsThruput(i))
                        total(14) = CDbl(total(14)) + CDbl(InsThruputhr(i))
                        total(15) = CDbl(total(15)) + CDbl(InsThruputkg(i))
                        total(16) = CDbl(total(16)) + CDbl(TotalLoad(i))
                        total(17) = CDbl(total(17)) + CDbl(Benchmark(i))
                        total(18) = CDbl(total(18)) + CDbl(Delta(i))
                        total(19) = CDbl(total(19)) + CDbl(CFE(i))
                    Next
                End If
            Catch ex As Exception
                Throw New Exception("E1InjectionCalc:InjectionMoldCalculate:" + ex.Message.ToString())
            End Try
        End Sub
    End Class
End Class
