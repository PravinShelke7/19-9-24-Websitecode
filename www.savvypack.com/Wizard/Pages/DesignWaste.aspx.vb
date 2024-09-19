#Region "Imports Classes"
Imports System
Imports System.Data
Imports Allied.GetData
Imports Allied.UpdateInsert
Imports AjaxControlToolkit
Imports System.Math
#End Region
Partial Class Pages_DesignWaste
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _iCaseID As Integer
    Dim _lErrorLble As Label
    Dim _bUpdateBtn As ImageButton


    Public Property CaseID() As Integer
        Get
            Return _iCaseID
        End Get
        Set(ByVal Value As Integer)
            _iCaseID = Value
        End Set
    End Property

    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UpdateBtn() As ImageButton
        Get
            Return _bUpdateBtn
        End Get
        Set(ByVal Value As ImageButton)
            _bUpdateBtn = Value
        End Set
    End Property
#End Region
#Region "MastePage Content Variables"
    Protected Sub GerErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GerUpdateButton()
        UpdateBtn = Page.Master.FindControl("imgUpdate")
        UpdateBtn.Attributes.Add("onclick", "return checkNumericAll();")
        AddHandler UpdateBtn.Click, AddressOf Update_Click
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GerUpdateButton()
            GerErrorLable()
            BindPref()
            txtCD1.Attributes.Add("OnBlur", "return GetCupDiameter();")
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub BindPref()
        Dim dsPref As New DataSet
        Dim objGetData As New GetData()
        Try
            dsPref = objGetData.GetPreferencesEcon(Session("SessionId").ToString(), Session("Modules"))
            lblCWeight.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
            lblCD.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
            lblShelf.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
            lblEdge.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
            lblTot.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE9").ToString() + ")"
            lblMOutPutU.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + "/hr)"
            lblPOutU.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + "/hr)"
            lblDesWU1.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + "/hr)"
            'lblCWeightUnit.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + "/hr)"
            lblTh.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE1").ToString() + ")"
            lblDW11.Text = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
            If Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("UNITS").ToString()) = 0.0 Then
                lblDV11.Text = "(in3)"
                lblRMAreaU.Text = "(in2)"
                lblProdAreaU.Text = "(in2)"
                lblDWaste1U.Text = "(in2)"
            Else
                lblDV11.Text = "(mm3)"
                lblRMAreaU.Text = "(mm2)"
                lblProdAreaU.Text = "(mm2)"
                lblDWaste1U.Text = "(mm2)"


            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Update()
        Catch ex As Exception
            ErrorLable.Text = "Co2 Wizard:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Update()
        Dim ds As New DataSet()
        Dim objGetData As New GetData()
        Dim dsPref As New DataSet
        Dim Cavity As Decimal
        Dim SWidthTotal As Decimal
        Dim SIndexTotal As Decimal
        Dim SheetArea As Decimal
        Dim CupArea As Decimal
        Dim DesignWaste As Decimal
        Dim DesignWasteD As Decimal
        Dim DesignWasteD1 As Decimal
        Dim CupWeight As Decimal
        Dim FPM As Decimal
        Dim lblPHour As Decimal
        Dim CupWeight1 As Decimal
        Dim DiscVol1 As Decimal
        Dim DiscVol2 As Decimal
        Dim DiscWeight1 As Decimal
        Try

            dsPref = objGetData.GetPreferencesEcon(Session("SessionId").ToString(), Session("Modules"))

            'Calculation of Cavity
            Cavity = Convert.ToDecimal(txtWidth.Text) * Convert.ToDecimal(txtIndex.Text)
           

            'Calculation of Sheet Width Total
            SWidthTotal = ((Convert.ToDecimal(txtCD1.Text) / Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())) * Convert.ToDecimal(txtWidth.Text)) + ((Convert.ToDecimal(txtShelf1.Text) / Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())) * (Convert.ToDecimal(txtWidth.Text) - 1)) + ((Convert.ToDecimal(txtEdge1.Text) / Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())) * 2)

            'Calculation of Sheet Index Total
            SIndexTotal = ((Convert.ToDecimal(txtCD1.Text) / Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())) * Convert.ToDecimal(txtIndex.Text)) + ((Convert.ToDecimal(txtShelf2.Text) / Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())) * (Convert.ToDecimal(txtIndex.Text) - 1)) + ((Convert.ToDecimal(txtEdge2.Text) / Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())) * 2)

            'Calculation of Sheet Area
            SheetArea = SWidthTotal * SIndexTotal

            'Calculation of Cup Area
            CupArea = Convert.ToDecimal(3.14159) * Pow(((Convert.ToDecimal(txtCD1.Text) / Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())) / 2), 2) * Cavity

            'Calculation of Design Waste
            DesignWaste = (SheetArea - CupArea)
            If SheetArea <> Convert.ToDecimal(0) Then
                DesignWasteD = DesignWaste / SheetArea * Convert.ToDecimal(100)
            Else
                DesignWasteD = 0
            End If


            'Calculation of FPM
            FPM = (SIndexTotal / Convert.ToDecimal(12)) * Convert.ToDecimal(txtCycle.Text)

            'Calculation Of Lb/Hr
            lblPHour = FPM * Convert.ToDecimal(12 * 60) * SWidthTotal * (Convert.ToDecimal((txtThick.Text) / Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())) / 1000) / Convert.ToDecimal(1728) * Convert.ToDecimal(62.4) * Convert.ToDecimal(txtGrav.Text)

            'Calculation of Disc volume
            DiscVol1 = Convert.ToDecimal(3.14159) * Pow(Convert.ToDecimal((txtCD1.Text / Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())) / 2), 2) * ((Convert.ToDecimal(txtThick.Text / Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString()))) / 1000)
            DiscVol2 = DiscVol1 / Convert.ToDecimal(1728)

            'Calculation of Disc Weight
            DiscWeight1 = DiscVol2 * Convert.ToDecimal(62.4) * Convert.ToDecimal(txtGrav.Text)

            'Calculation of Cup Weight
            CupWeight = Cavity * Convert.ToDecimal(txtCycle.Text) * DiscWeight1 * Convert.ToDecimal(60)
            If lblPHour <> Convert.ToDecimal(0) Then
                CupWeight1 = CupWeight / lblPHour
                DesignWasteD1 = (1 - CupWeight1) * 100
            Else
                CupWeight1 = 0.0
                DesignWasteD1 = 0.0
            End If





            'Set the values
            lblCavVal.Text = Cavity.ToString()
            lblTot1.Text = FormatNumber((SWidthTotal * Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())).ToString(), 3)
            lblTot2.Text = FormatNumber((SIndexTotal * Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())).ToString(), 3)
            lblFpm.Text = FormatNumber(FPM.ToString(), 3)




            ' lblDesWaste.Text = FormatNumber(DesignWasteD1.ToString(), 3)

            If Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("UNITS").ToString()) = 0.0 Then
                lblDV1.Text = FormatNumber(DiscVol1.ToString(), 3)
                lnlRMArea.Text = FormatNumber(SheetArea.ToString(), 3)
                lblProdArea.Text = FormatNumber(CupArea.ToString(), 3)
                lblDWaste1.Text = FormatNumber(DesignWaste.ToString(), 3)

            Else
                lblDV1.Text = FormatNumber((DiscVol1.ToString() * 16387.064), 3)
                lnlRMArea.Text = FormatNumber(SheetArea.ToString() * 645.16, 3)
                lblProdArea.Text = FormatNumber(CupArea.ToString() * 645.16, 3)
                lblDWaste1.Text = FormatNumber(DesignWaste.ToString() * 645.16, 3)
            End If
            lblDWaste2.Text = FormatNumber(DesignWasteD.ToString(), 3)

            lblMOutPut.Text = FormatNumber((lblPHour * Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVWT").ToString())).ToString(), 3)
            lblPOut.Text = FormatNumber((CupWeight * Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVWT").ToString())), 3)
            lblDesW1.Text = FormatNumber(Convert.ToDecimal(lblMOutPut.Text - lblPOut.Text).ToString(), 3)
            If (Convert.ToDecimal(lblMOutPut.Text) <> Convert.ToDecimal(0)) Then
                lblDesW2.Text = FormatNumber((Convert.ToDecimal(lblDesW1.Text / lblMOutPut.Text).ToString()) * 100, 3)
            Else
                lblDesW2.Text = FormatNumber(0, 3)
            End If

            'lblDV2.Text = FormatNumber(DiscVol2.ToString(), 3)
            lblDW1.Text = FormatNumber((DiscWeight1 * Convert.ToDecimal(dsPref.Tables(0).Rows(0).Item("CONVWT").ToString())).ToString(), 5)




        Catch ex As Exception

        End Try
    End Sub

  
End Class
