
Partial Class Session
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ID As String = Request.QueryString("ID")
        ' Dim Moduls As String = Request.QueryString("Module")
        Dim Moduls As String = Request.QueryString("Moduls")

        Dim UserName As String = ""
        Dim S2Chrt As String = String.Empty
        Dim S1Chrt As String = String.Empty
	Dim E2Chrt As String = String.Empty
        Dim E1Chrt As String = String.Empty
        Session("Back") = "Secure"
        If ID = "Chart" Then
            UserName = Request.QueryString("UserName")
            Session("User") = UserName
            Response.Redirect("Charts/DiffrentCharts.aspx?ID=" + Session("User") + "")
        ElseIf ID = "Material" Then
            UserName = Request.QueryString("UserName")
            Session("User") = UserName
            Response.Redirect("Charts/MaterialPrice.aspx")
        ElseIf ID = "Salary" Then
            UserName = Request.QueryString("UserName")
            Session("User") = UserName
            Response.Redirect("Charts/SalaryPrice.aspx")
        ElseIf ID = "Energy" Then
            UserName = Request.QueryString("UserName")
            Session("User") = UserName
            Response.Redirect("Charts/EnergyPrice.aspx")
	ElseIf ID = "Econ1" Then
            Session("User") = Request.QueryString("UserName")
            Session("C1") = Request.QueryString("C1")
            Session("C2") = Request.QueryString("C2")
            E1Chrt = Request.QueryString("CT")
            If E1Chrt = "CRCOST" Then
                Response.Redirect("Charts/E1Charts/CRCost.aspx?IsDep=N")
            ElseIf E1Chrt = "CRCOSTD" Then
                Response.Redirect("Charts/E1Charts/CRCost.aspx?IsDep=Y")
            ElseIf E1Chrt = "CSCOST" Then
                Response.Redirect("Charts/E1Charts/CSCost.aspx?IsDep=N")
            ElseIf E1Chrt = "CSCOSTD" Then
                Response.Redirect("Charts/E1Charts/CSCost.aspx?IsDep=Y")
            ElseIf E1Chrt = "CRPRFT" Then
                Response.Redirect("Charts/E1Charts/CRProftAndLoss.aspx?IsDep=N")
            ElseIf E1Chrt = "CRPRFTD" Then
                Response.Redirect("Charts/E1Charts/CRProftAndLoss.aspx?IsDep=Y")
            End If
        ElseIf ID = "Econ2" Then
            Session("User") = Request.QueryString("UserName")
            Session("C1") = Request.QueryString("C1")
            Session("C2") = Request.QueryString("C2")
            E2Chrt = Request.QueryString("CT")
            If E2Chrt = "CRCOST" Then
                Response.Redirect("Charts/E2Charts/CRCost.aspx?IsDep=N")
            ElseIf E2Chrt = "CRCOSTD" Then
                Response.Redirect("Charts/E2Charts/CRCost.aspx?IsDep=Y")
            ElseIf E2Chrt = "CSCOST" Then
                Response.Redirect("Charts/E2Charts/CSCost.aspx?IsDep=N")
            ElseIf E2Chrt = "CSCOSTD" Then
                Response.Redirect("Charts/E2Charts/CSCost.aspx?IsDep=Y")
            ElseIf E2Chrt = "CRPRFT" Then
                Response.Redirect("Charts/E2Charts/CRProftAndLoss.aspx?IsDep=N")
            ElseIf E2Chrt = "CRPRFTD" Then
                Response.Redirect("Charts/E2Charts/CRProftAndLoss.aspx?IsDep=Y")
            End If
        ElseIf ID = "Sustain1" Then
            UserName = Request.QueryString("UserName")
            Session("User") = UserName
            Session("C1") = Request.QueryString("C1")
            Session("C2") = Request.QueryString("C2")
            S1Chrt = Request.QueryString("CT")
            If S1Chrt = "TE" Then
                Response.Redirect("Charts/S1Charts/TotErgyCC.aspx")
            ElseIf S1Chrt = "EPW" Then
                Response.Redirect("Charts/S1Charts/ErgyPWtCC.aspx")
            ElseIf S1Chrt = "EPU" Then
                Response.Redirect("Charts/S1Charts/ErgyPUnitCC.aspx")
            ElseIf S1Chrt = "TG" Then
                Response.Redirect("Charts/S1Charts/TotGHGCC.aspx")
            ElseIf S1Chrt = "CEC" Then
                Response.Redirect("Charts/S1Charts/CEnergyCharts.aspx")
            ElseIf S1Chrt = "CESTC" Then
                Response.Redirect("Charts/S1Charts/CStackErgyChart.aspx")
            ElseIf S1Chrt = "GPW" Then
                Response.Redirect("Charts/S1Charts/GHGPwtCC.aspx")
            ElseIf S1Chrt = "GPU" Then
                Response.Redirect("Charts/S1Charts/GHGPUnitCC.aspx")
            ElseIf S1Chrt = "CGC" Then
                Response.Redirect("Charts/S1Charts/CGHGCharts.aspx")
            ElseIf S1Chrt = "CGSTC" Then
                Response.Redirect("Charts/S1Charts/CStackGHGChart.aspx")
            End If
        ElseIf ID = "Sustain2" Then
            UserName = Request.QueryString("UserName")
            Session("User") = UserName
            Session("C1") = Request.QueryString("C1")
            Session("C2") = Request.QueryString("C2")
            S2Chrt = Request.QueryString("CT")
            If S2Chrt = "TE" Then
                Response.Redirect("Charts/S2Charts/TotErgyCC.aspx")
            ElseIf S2Chrt = "EPW" Then
                Response.Redirect("Charts/S2Charts/ErgyPWtCC.aspx")
            ElseIf S2Chrt = "EPU" Then
                Response.Redirect("Charts/S2Charts/ErgyPUnitCC.aspx")
            ElseIf S2Chrt = "CEC" Then
                Response.Redirect("Charts/S2Charts/CEnergyCharts.aspx")
            ElseIf S2Chrt = "CESTC" Then
                Response.Redirect("Charts/S2Charts/CStackErgyChart.aspx")
            ElseIf S2Chrt = "TG" Then
                Response.Redirect("Charts/S2Charts/TotGHGCC.aspx")
            ElseIf S2Chrt = "GPW" Then
                Response.Redirect("Charts/S2Charts/GHGPwtCC.aspx")
            ElseIf S2Chrt = "GPU" Then
                Response.Redirect("Charts/S2Charts/GHGPUnitCC.aspx")
            ElseIf S2Chrt = "CGC" Then
                Response.Redirect("Charts/S2Charts/CGHGCharts.aspx")
            ElseIf S2Chrt = "CGSTC" Then
                Response.Redirect("Charts/S2Charts/CStackGHGChart.aspx")
            End If
        Else
            'Session("ID") = ID
            If Moduls = "E1" Or Moduls = "COMP" Then
                'Response.Redirect("Pages/Econ1/Default.aspx")
                If Session("SavvyModType") <> "1" Then
                    Response.Redirect("Pages/Econ1/Default2.aspx")
                Else
                    Response.Redirect("Pages/Econ1/Default.aspx")
                End If
            ElseIf Moduls = "E2" Then
                'Response.Redirect("Pages/Econ2/Default.aspx")
                If Session("SavvyModType") <> "1" Then
                    Response.Redirect("Pages/Econ2/Default2.aspx")
                Else
                    Response.Redirect("Pages/Econ2/Default.aspx")
                End If
            ElseIf Moduls = "E3" Then
                Response.Redirect("Pages/Econ3/Default.aspx")
            ElseIf Moduls = "E4" Then
                Response.Redirect("Pages/Econ4/Default.aspx")
            ElseIf Moduls = "EC1" Then
                Response.Redirect("Pages/Echem1/Default.aspx")
            ElseIf Moduls = "SC1" Then
                Response.Redirect("Pages/SChem1/Default.aspx")
            ElseIf Moduls = "S1" Or Moduls = "COMPS1" Then
                ' Response.Redirect("Pages/Sustain1/Default.aspx")
                If Session("SavvyModType") <> "1" Then
                    Response.Redirect("Pages/Sustain1/Default2.aspx")
                Else
                    Response.Redirect("Pages/Sustain1/Default.aspx")
                End If
            ElseIf Moduls = "S2" Then
                'Response.Redirect("Pages/Sustain2/Default.aspx")
                If Session("SavvyModType") <> "1" Then
                    Response.Redirect("Pages/Sustain2/Default2.aspx")
                Else
                    Response.Redirect("Pages/Sustain2/Default.aspx")
                End If
            ElseIf Moduls = "S3" Then
                Response.Redirect("Pages/Sustain3/Default.aspx")
            ElseIf Moduls = "S4" Then
                Response.Redirect("Pages/Sustain4/Default.aspx")
            ElseIf Moduls = "M1" Then
                Response.Redirect("Pages/Market1/Default.aspx")
            ElseIf Moduls = "SPEC" Then
                Response.Redirect("Pages/Spec/Default.aspx")
            ElseIf Moduls = "CONTR" Then
                Response.Redirect("Pages/Contract/Search.aspx", True)
            ElseIf Moduls = "DIST" Then
                Response.Redirect("Pages/Distribution/Default.aspx", True)
            ElseIf Moduls = "SDIST" Then
                Response.Redirect("Pages/SDistribution/Default.aspx", True)
            ElseIf Moduls = "VChain" Then
                Response.Redirect("Pages/ValueChain/Default.aspx")
            ElseIf Moduls = "RETL" Then
                Response.Redirect("Pages/Retail/Default.aspx", True)
            ElseIf Moduls = "PACKPROD" Then
                Response.Redirect("Pages/PackProd/Search.aspx", True)
            ElseIf Moduls = "StandAssist" Then
                Response.Redirect("Pages/StandAssist/Default.aspx")
            ElseIf Moduls = "MEDECON1" Then
                Response.Redirect("Pages/Med1/Default.aspx")
            ElseIf Moduls = "MEDECON2" Then
                Response.Redirect("Pages/Med2/Default.aspx")
            ElseIf Moduls = "MEDSUSTAIN1" Then
                Response.Redirect("Pages/SMed1/Default.aspx")
            ElseIf Moduls = "MEDSUSTAIN2" Then
                Response.Redirect("Pages/SMed2/Default.aspx")
			ElseIf Moduls = "MOLDE1" Then
                Response.Redirect("Pages/MoldEcon1/Default.aspx")
            ElseIf Moduls = "MOLDE2" Then
                Response.Redirect("Pages/MoldEcon2/Default.aspx")
            ElseIf Moduls = "MOLDS1" Then
                Response.Redirect("Pages/MoldSustain1/Default.aspx")
            ElseIf Moduls = "MOLDS2" Then
                Response.Redirect("Pages/MoldSustain2/Default.aspx")
            ElseIf Moduls = "EMON" Then
                Response.Redirect("Pages/Emonitor/Default.aspx")
            ElseIf Moduls = "IContract" Then
                Response.Redirect("Pages/SavvyPackPro/Default.aspx")
            End If
        End If
    End Sub
End Class
