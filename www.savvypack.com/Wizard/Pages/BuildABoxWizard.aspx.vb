#Region "Imports Classes"
Imports System
Imports System.Data
Imports Allied.GetData
Imports Allied.UpdateInsert
Imports AjaxControlToolkit
Imports System.Math
#End Region
Partial Class Pages_BuildABoxWizard
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

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            CalCulate()
        Catch ex As Exception
            ErrorLable.Text = "BuildABoxWizard:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

#End Region

#Region "MastePage Content Variables"
    Protected Sub GerErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GerUpdateButton()
        UpdateBtn = Page.Master.FindControl("imgUpdate")
        AddHandler UpdateBtn.Click, AddressOf Update_Click
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GerUpdateButton()
            GerErrorLable()
           
            If Not IsPostBack Then
                SetUnits()
                GetPackageDetails()
                GetCartonFluteDetails()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPackageDetails()
        Try
            Dim objGetData As New GetData
            Dim ds As New DataSet
            ds = objGetData.GetPackageDetails()
            Session("packds") = ds
            With ddlPackFormat
                .DataSource = ds
                .DataTextField = "FORMATDE"
                .DataValueField = "FORMATID"
                .DataBind()
            End With
            GetPackageDimntion()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetCartonFluteDetails()
        Try
            Dim objGetData As New GetData()
            Dim ds As New DataSet
            ds = objGetData.GetCartonFluteType()
            Session("FluteType") = ds
            With ddlCFluteType
                .DataSource = ds
                .DataTextField = "FLUTEDE"
                .DataValueField = "FLUTEID"
                .DataBind()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPackageDimntion()
        Try
            Dim ds As New DataSet
            Dim foundRows As DataRow()
            ds = Session("packds")
            foundRows = ds.Tables(0).Select("FORMATID = " + ddlPackFormat.SelectedValue.ToString() + "")

            lblPM1.Text = foundRows(0)("M1").ToString().Replace("(in)", "")
            lblPM2.Text = foundRows(0)("M2").ToString().Replace("(in)", "")
            lblPM3.Text = foundRows(0)("M3").ToString().Replace("(in)", "")

            If lblPM3.Text = "Not Required" Then
                txtPM3.Enabled = False
            Else
                txtPM3.Enabled = True
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LayerConfigrationCalc()
        Try
            Dim _iNoPacksPerCLay As New Decimal
            Dim _iNoPackPerC As New Decimal
            txtlayerN1.Text = FormatNumber(Convert.ToDecimal(txtlayerN1.Text.ToString()), 2)
            txtlayerN2.Text = FormatNumber(Convert.ToDecimal(txtlayerN2.Text.ToString()), 2)
            txtNLPC.Text = FormatNumber(Convert.ToDecimal(txtNLPC.Text.ToString()), 2)

            _iNoPacksPerCLay = Convert.ToDecimal(txtlayerN1.Text.ToString()) * Convert.ToDecimal(txtlayerN2.Text.ToString())
            _iNoPackPerC = _iNoPacksPerCLay * Convert.ToDecimal(txtNLPC.Text.ToString())

            lblNPPL.Text = FormatNumber(_iNoPacksPerCLay, 2)
            lblNPPC.Text = FormatNumber(_iNoPackPerC, 2)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CartonDimentionCalc()
        Try
            Dim ds As New DataSet
            Dim dsPref As New DataSet
            Dim foundRows As DataRow()
            Dim _iCWidth As New Decimal
            Dim _iCLength As New Decimal
            Dim _iCHeight As New Decimal
            Dim _iPWidth As New Decimal
            Dim _iPLength As New Decimal
            Dim _iPHeight As New Decimal
            Dim _iCArea As New Decimal

            ds = Session("packds")
            dsPref = Session("dspref")
            foundRows = ds.Tables(0).Select("FORMATID = " + ddlPackFormat.SelectedValue.ToString() + "")
            _iPWidth = Convert.ToDecimal(txtPM1.Text / Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVTHICK").ToString()))
            _iPLength = Convert.ToDecimal(txtPM2.Text / Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVTHICK").ToString()))
            _iPHeight = Convert.ToDecimal(txtPM3.Text / Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVTHICK").ToString()))





            If foundRows(0)("FORMATDE").ToString().Trim() = "Bottle" Then
                _iCWidth = (_iPWidth * Convert.ToDecimal(txtlayerN1.Text.ToString()))
                _iCLength = (_iPWidth * Convert.ToDecimal(txtlayerN2.Text.ToString()))
                _iCHeight = (_iPLength * Convert.ToDecimal(txtNLPC.Text.ToString()))
            ElseIf foundRows(0)("FORMATDE").ToString().Trim() = "Can" Then
                _iCWidth = (_iPWidth * Convert.ToDecimal(txtlayerN1.Text.ToString()))
                _iCLength = (_iPWidth * Convert.ToDecimal(txtlayerN2.Text.ToString()))
                _iCHeight = (_iPLength * Convert.ToDecimal(txtNLPC.Text.ToString()))
            ElseIf foundRows(0)("FORMATDE").ToString().Trim() = "Folding Carton" Then
                _iCWidth = (_iPWidth * Convert.ToDecimal(txtlayerN1.Text.ToString()))
                _iCLength = (_iPLength * Convert.ToDecimal(txtlayerN2.Text.ToString()))
                _iCHeight = (_iPHeight * Convert.ToDecimal(txtNLPC.Text.ToString()))

            End If

            _iCArea = ((2 * _iCWidth * _iCLength) + (2 * _iCLength * _iCHeight)) + (2 * _iCWidth * _iCHeight)


            txtPM1.Text = FormatNumber(Convert.ToDecimal(_iPWidth) * Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVTHICK").ToString()), 2)
            txtPM2.Text = FormatNumber(Convert.ToDecimal(_iPLength) * Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVTHICK").ToString()), 2)
            txtPM3.Text = FormatNumber(Convert.ToDecimal(_iPHeight) * Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVTHICK").ToString()), 2)

            lblCCM1.Text = FormatNumber(Convert.ToDecimal(_iCWidth) * Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVTHICK").ToString()), 2)
            lblCCM2.Text = FormatNumber(Convert.ToDecimal(_iCLength) * Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVTHICK").ToString()), 2)
            lblCCM3.Text = FormatNumber(Convert.ToDecimal(_iCHeight) * Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVTHICK").ToString()), 2)


            lblCCArea.Text = FormatNumber(Convert.ToDecimal(_iCArea) * (Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVAREA").ToString())), 2)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CartonCostAreaCalc()
        Try
            Dim ds As New DataSet
            Dim dsPref As New DataSet
            Dim dsConversion As DataSet
            Dim foundRows As DataRow()
            Dim objGetData As New GetData()
            Dim _iCArea As New Decimal
            Dim _iCCCost As New Decimal
            Dim _iCCWt As New Decimal
            ds = Session("FluteType")
            dsPref = Session("dspref")
            dsConversion = objGetData.GetConversionFactor()
            _iCArea = Convert.ToDecimal(lblCCArea.Text)
            _iCArea = _iCArea / Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVAREA").ToString())
            foundRows = ds.Tables(0).Select("FLUTEID = " + ddlCFluteType.SelectedValue.ToString() + "")
            _iCCCost = (Convert.ToDecimal(foundRows(0)("FLUTECOST").ToString()) / 1000) * Convert.ToDecimal(dsConversion.Tables(0).Rows(0)("IN2PSQFT")) * _iCArea
            _iCCWt = (Convert.ToDecimal(foundRows(0)("FLUTEWEIGHT").ToString()) / 1000) * Convert.ToDecimal(dsConversion.Tables(0).Rows(0)("IN2PSQFT")) * _iCArea

            lblCCWt.Text = FormatNumber(Convert.ToDecimal(_iCCWt) * (Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVWT").ToString())), 3)
            If Session("Modules") = "E1" Or Session("Modules") = "E2" Or Session("Modules") = "MED1" Or Session("Modules") = "MED2" Then
                Dim DsPrefC As New DataSet
                DsPrefC = Session("dsEpref")
                lblCCCost.Text = FormatNumber(Convert.ToDecimal(_iCCCost) * Convert.ToDecimal(DsPrefC.Tables(0).Rows(0)("CURR").ToString()), 3)

            Else
                LCIDetails()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LCIDetails()
        Dim dsPref As New DataSet
        Dim dsLci As New DataSet
        Dim objGetData As New GetData()
        Try
            dsPref = Session("dspref")
            dsLci = objGetData.GetLCIData(Session("SessionId"), Session("Modules"))
            lblGHG.Text = FormatNumber(Convert.ToDecimal(dsLci.Tables(0).Rows(0).Item("GHG").ToString()), 2)
            lblErgy.Text = FormatNumber((Convert.ToDecimal(dsLci.Tables(0).Rows(0).Item("ERGY").ToString())) / (Convert.ToDecimal(dsPref.Tables(0).Rows(0)("CONVWT").ToString())), 2)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CalCulate()
        Try
            SetUnits()
            LayerConfigrationCalc()
            CartonDimentionCalc()
            CartonCostAreaCalc()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SetUnits()
        Try
            Dim objGetData As New GetData()
            Dim ds As New DataSet
            ds = objGetData.GetBbPreferences(Session("SessionId"), Session("Modules"))
            Session("dspref") = ds
            If Convert.ToInt32(ds.Tables(0).Rows(0)("UNITS").ToString()) = 0 Then
                lblUnitM1.Text = "(in)"
                lblUnitM2.Text = "(in)"
                lblUnitM3.Text = "(in)"
                lblUnitCCM1.Text = "(in)"
                lblUnitCCM2.Text = "(in)"
                lblUnitCCM3.Text = "(in)"
                lblUnitCCArea.Text = "(msi)"
                'lblCCCost.Text = "()"
                lblUnitCCWt.Text = "(lb)"
                lblGHGU.Text = "(lb/lb Mat.)"
                lblErgyU.Text = "(MJ/lb Mat.)"


            Else
                lblUnitM1.Text = "(mm)"
                lblUnitM2.Text = "(mm)"
                lblUnitM3.Text = "(mm)"
                lblUnitCCM1.Text = "(mm)"
                lblUnitCCM2.Text = "(mm)"
                lblUnitCCM3.Text = "(mm)"
                lblUnitCCArea.Text = "(m2)"
                lblUnitCCWt.Text = "(kg)"
                lblGHGU.Text = "(kg/kg Mat.)"
                lblErgyU.Text = "(MJ/kg Mat.)"
            End If

            If Session("Modules") = "E1" Or Session("Modules") = "E2" Or Session("Modules").ToString().ToUpper() = "ECHEM1" Or Session("Modules").ToString().ToUpper() = "EDIST" Or Session("Modules") = "MED1" Or Session("Modules") = "MED2" Then
                Dim DsPrefC As New DataSet
                DsPrefC = objGetData.GetEPreferences(Session("SessionId"), Session("Modules"))
                Session("dsEpref") = DsPrefC
                lblUnitCCost.Text = DsPrefC.Tables(0).Rows(0)("TITLE2").ToString() + "/unit"

                tdCost1.Visible = True
                tdCost2.Visible = True
                tdCost3.Visible = True

                tdLCI1.Visible = False
                tdLCI2.Visible = False
                tdLCI3.Visible = False
            Else

                tdCost1.Visible = False
                tdCost2.Visible = False
                tdCost3.Visible = False

                tdLCI1.Visible = True
                tdLCI2.Visible = True
                tdLCI3.Visible = True

            End If

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub ddlPackFormat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPackFormat.SelectedIndexChanged
        Try
            GetPackageDimntion()
        Catch ex As Exception

        End Try
    End Sub

   
End Class
