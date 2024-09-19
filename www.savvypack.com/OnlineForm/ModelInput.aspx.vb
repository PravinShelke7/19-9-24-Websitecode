Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Reflection

Partial Class Pages_ModelInput
    Inherits System.Web.UI.Page
    Dim ProdCount As Integer
    Dim SizeCount As Integer
    Dim TypeCount As Integer
    Dim GeogCount As Integer
    Dim ChainCount As Integer
    Dim Feat1Count As Integer
    Dim Feat2Count As Integer
    Dim ProduCount As Integer
    Dim PackCount As Integer
    Dim GraphCount As Integer
    Dim StructCount As Integer
    Dim FormCount As Integer
    Dim ManufCount As Integer
    Dim PackgCount As Integer
    Dim SkuCount As Integer
    Dim ColCount As Integer = 2
    Dim objUpIns As New UpdateInsert()


#Region "Get Set Variables"
    Dim _lErrorLble As Label

    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

#End Region


#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_MODELINPUT")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            Session("ProjectId") = Request.QueryString("ProjectId")
            hidProjectId.Value = Request.QueryString("ProjectId")
            GetProjectDetails()
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            lblTag.Attributes.Add("align", "center")
            If Not IsPostBack Then
                hidSortId.Value = "0"
                Session("SeqCnt") = "1"
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Opened Project Input Details Page", Session("ProjectId"), Session("SPROJLogInCount").ToString())

                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

                'If Session("SavvyAnalyst") = "Y" Then
                '    btnSave.Visible = True
                'End If
                '
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        btnGrid.Enabled = False
                        btnSheet.Enabled = False
                        'grdModel.Enabled = False
                    End If
                End If
                ' form1.Disabled = True
            End If
            PageDetails()
            'DisableControls(Page)
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Private Sub DisableControls(ByVal control As System.Web.UI.Control)

        For Each c As System.Web.UI.Control In control.Controls

            ' Get the Enabled property by reflection.
            Dim type As Type = c.GetType
            Dim prop As PropertyInfo = type.GetProperty("Enabled")

            ' Set it to False to disable the control.
            If Not prop Is Nothing Then
                prop.SetValue(c, False, Nothing)
            End If

            ' Recurse into child controls.
            If c.Controls.Count > 0 Then
                Me.DisableControls(c)
            End If

        Next

    End Sub

    Public Sub PageDetails()
        GetProductDetails()
        GetSizeDetails()
        GetTypeDetails()
        GetGeographyDetails()
        GetChainDetails()
        GetFeat1Details()
        GetFeat2Details()
        GetProducerDetails()
        GetPackerDetails()
        GetGraphicDetails()
        GetStructureDetails()
        GetFormulaDetails()
        GetManufDetails()
        GetPackgDetails()
        GetSkuDetails()
        If Not IsPostBack Then
            GetModelDetails()
        End If
    End Sub

    Protected Sub GetProjectDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Try
            ds = objGetData.GetExistProjectDetails(Session("ProjectId"))
            lblNum.Text = ds.Tables(0).Rows(0).Item("PROJECTID").ToString()
            lblTitle.Text = ds.Tables(0).Rows(0).Item("TITLE").ToString()

        Catch ex As Exception
            ErrorLable.Text = "Error:GetProjectDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetModelDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim chkBx As CheckBox
        Dim PFlag As Boolean = False
        Dim SFlag As Boolean = False
        Dim TFlag As Boolean = False
        Dim GFlag As Boolean = False
        Dim CFlag As Boolean = False
        Dim F1Flag As Boolean = False
        Dim F2Flag As Boolean = False
        Dim PdFlag As Boolean = False
        Dim PackFlag As Boolean = False
        Dim GrFlag As Boolean = False
        Dim StFlag As Boolean = False
        Dim FrFlag As Boolean = False
        Dim MfFlag As Boolean = False
        Dim PgFlag As Boolean = False
        Dim SkuFlag As Boolean = False
        Try

            For j = 1 To ProdCount
                Dim cntl As Control = FindControl("chkBxProd" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    PFlag = True
                End If
            Next

            For j = 1 To SizeCount
                Dim cntl As Control = FindControl("chkBxSize" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    SFlag = True
                End If
            Next

            For j = 1 To TypeCount
                Dim cntl As Control = FindControl("chkBxType" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    TFlag = True
                End If
            Next

            For j = 1 To GeogCount
                Dim cntl As Control = FindControl("chkBxGeog" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    GFlag = True
                End If
            Next

            For j = 1 To ChainCount
                Dim cntl As Control = FindControl("chkBxChain" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    CFlag = True
                End If
            Next

            For j = 1 To Feat1Count
                Dim cntl As Control = FindControl("chkBxFeat1" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    F1Flag = True
                End If
            Next

            For j = 1 To Feat2Count
                Dim cntl As Control = FindControl("chkBxFeat2" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    F2Flag = True
                End If
            Next

            For j = 1 To ProduCount
                Dim cntl As Control = FindControl("chkBxProdu" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    PdFlag = True
                End If
            Next

            For j = 1 To PackCount
                Dim cntl As Control = FindControl("chkBxPack" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    PackFlag = True
                End If
            Next

            For j = 1 To GraphCount
                Dim cntl As Control = FindControl("chkBxGraph" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    GrFlag = True
                End If
            Next

            For j = 1 To StructCount
                Dim cntl As Control = FindControl("chkBxStruct" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    StFlag = True
                End If
            Next

            For j = 1 To FormCount
                Dim cntl As Control = FindControl("chkBxFormula" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    FrFlag = True
                End If
            Next

            For j = 1 To ManufCount
                Dim cntl As Control = FindControl("chkBxManuf" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    MfFlag = True
                End If
            Next

            For j = 1 To PackgCount
                Dim cntl As Control = FindControl("chkBxPackg" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    PgFlag = True
                End If
            Next

            For j = 1 To SkuCount
                Dim cntl As Control = FindControl("chkBxSku" + j.ToString())
                chkBx = CType(cntl, CheckBox)
                If chkBx.Checked Then
                    SkuFlag = True
                End If
            Next

            'ds = objGetData.GetModelDetails(Session("ProjectId"), PFlag, SFlag, TFlag, GFlag, CFlag, F1Flag, F2Flag, PdFlag, PackFlag, GrFlag, StFlag, FrFlag, MfFlag, PgFlag, SkuFlag)
            ds = objGetData.GetModelDetails(Session("ProjectId"))

            If ds.Tables(0).Rows.Count > 0 Then
                lblCount.Visible = True
                dv = ds.Tables(0).DefaultView()
                dv.RowFilter = "ISACTIVE='Y'"
                dt = dv.ToTable()
                lblCount.Text = "Number of models for the projects: " + dt.Rows.Count.ToString()
                divModel.Visible = True
                If ds.Tables(0).Rows.Count = 1 Then
                    divModel.Attributes.Add("style", "height: 113px; overflow: auto;  background-color: White;")
                ElseIf ds.Tables(0).Rows.Count = 2 Then
                    divModel.Attributes.Add("style", "height: 142px; overflow: auto;  background-color: White;")
                ElseIf ds.Tables(0).Rows.Count = 3 Then
                    divModel.Attributes.Add("style", "height: 170px; overflow: auto;  background-color: White;")
                ElseIf ds.Tables(0).Rows.Count = 4 Then
                    divModel.Attributes.Add("style", "height: 200px; overflow: auto;  background-color: White;")
                ElseIf ds.Tables(0).Rows.Count = 5 Then
                    divModel.Attributes.Add("style", "height: 225px; overflow: auto;  background-color: White;")
                ElseIf ds.Tables(0).Rows.Count = 6 Then
                    divModel.Attributes.Add("style", "height: 255px; overflow: auto;  background-color: White;")
                ElseIf ds.Tables(0).Rows.Count = 7 Then
                    divModel.Attributes.Add("style", "height: 280px; overflow: auto;  background-color: White;")
                ElseIf ds.Tables(0).Rows.Count = 8 Then
                    divModel.Attributes.Add("style", "height: 306px; overflow: auto;  background-color: White;")
                ElseIf ds.Tables(0).Rows.Count = 9 Then
                    divModel.Attributes.Add("style", "height: 337px; overflow: auto;  background-color: White;")
                ElseIf ds.Tables(0).Rows.Count = 10 Then
                    divModel.Attributes.Add("style", "height: 365px; overflow: auto;  background-color: White;")
                ElseIf ds.Tables(0).Rows.Count > 10 Then
                    divModel.Attributes.Add("style", "height: 365px; overflow: auto;  background-color: White;")
                End If
                'divModel.Attributes.Add("height", "auto")
                If TFlag = False Then
                    grdModel.Columns(3).Visible = False
                Else
                    dv.RowFilter = "PACKTYPE IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(3).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(3).Visible = False
                    End If

                End If
                If CFlag = False Then
                    grdModel.Columns(4).Visible = False
                Else
                    dv.RowFilter = "VCHAIN IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(4).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(4).Visible = False
                    End If

                End If
                If SFlag = False Then
                    grdModel.Columns(5).Visible = False
                Else
                    dv.RowFilter = "PACKSIZE IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(5).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(5).Visible = False
                    End If

                End If
                If PFlag = False Then
                    grdModel.Columns(6).Visible = False
                Else
                    dv.RowFilter = "PRODPACK IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(6).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(6).Visible = False
                    End If

                End If
                If GFlag = False Then
                    grdModel.Columns(7).Visible = False
                Else
                    dv.RowFilter = "GEOGRAPHY IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(7).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(7).Visible = False
                    End If

                End If
                If F1Flag = False Then
                    grdModel.Columns(8).Visible = False
                Else
                    dv.RowFilter = "SPFET1 IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(8).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(8).Visible = False
                    End If

                End If
                If F2Flag = False Then
                    grdModel.Columns(9).Visible = False
                Else
                    dv.RowFilter = "SPFET2 IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(9).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(9).Visible = False
                    End If

                End If
                If PdFlag = False Then
                    grdModel.Columns(10).Visible = False
                Else
                    dv.RowFilter = "PRODUCER IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(10).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(10).Visible = False
                    End If

                End If
                If PackFlag = False Then
                    grdModel.Columns(11).Visible = False
                Else
                    dv.RowFilter = "PACKER IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(11).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(11).Visible = False
                    End If

                End If
                If GrFlag = False Then
                    grdModel.Columns(12).Visible = False
                Else
                    dv.RowFilter = "GRAPHICS IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(12).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(12).Visible = False
                    End If

                End If
                If StFlag = False Then
                    grdModel.Columns(13).Visible = False
                Else
                    dv.RowFilter = "STRUCTURES IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(13).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(13).Visible = False
                    End If

                End If
                If FrFlag = False Then
                    grdModel.Columns(14).Visible = False
                Else
                    dv.RowFilter = "FORMULA IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(14).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(14).Visible = False
                    End If

                End If
                If MfFlag = False Then
                    grdModel.Columns(15).Visible = False
                Else
                    dv.RowFilter = "MANUFACTORY IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(15).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(15).Visible = False
                    End If

                End If
                If PgFlag = False Then
                    grdModel.Columns(16).Visible = False
                Else
                    dv.RowFilter = "PACKAGING IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(16).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(16).Visible = False
                    End If

                End If
                If SkuFlag = False Then
                    grdModel.Columns(17).Visible = False
                Else
                    dv.RowFilter = "SKU IS NOT NULL"
                    dt = dv.ToTable()
                    If dt.Rows.Count > 0 Then
                        grdModel.Columns(17).Visible = True
                        ColCount = ColCount + 1
                    Else
                        grdModel.Columns(17).Visible = False
                    End If

                End If

                If ColCount = 3 Then
                    grdModel.Width = 300
                ElseIf ColCount = 4 Then
                    grdModel.Width = 400
                ElseIf ColCount = 5 Then
                    grdModel.Width = 500
                ElseIf ColCount = 6 Then
                    grdModel.Width = 600
                ElseIf ColCount = 7 Then
                    grdModel.Width = 700
                ElseIf ColCount = 8 Then
                    grdModel.Width = 1330
                ElseIf ColCount = 9 Then
                    grdModel.Width = 1330
                ElseIf ColCount = 10 Then
                    grdModel.Width = 1330
                ElseIf ColCount = 11 Then
                    grdModel.Width = 1330
                ElseIf ColCount = 12 Then
                    grdModel.Width = 1330
                ElseIf ColCount = 13 Then
                    grdModel.Width = 1400
                ElseIf ColCount = 14 Then
                    grdModel.Width = 1500
                ElseIf ColCount = 15 Then
                    grdModel.Width = 1700
                ElseIf ColCount = 16 Then
                    grdModel.Width = 1800
                ElseIf ColCount = 17 Then
                    grdModel.Width = 1900
                End If
            Else
                divModel.Visible = False
                lblCount.Visible = False
            End If
            Session("ModelData") = ds
            grdModel.DataSource = ds

            grdModel.DataBind()

            BindLink()
        Catch ex As Exception
            ErrorLable.Text = "Error:GetModelDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindLink()
        Dim lnkProd As New LinkButton
        Dim lnkPack As New LinkButton
        Dim lnkGraph As New LinkButton
        Dim lnkStruct As New LinkButton
        Dim lnkFormula As New LinkButton
        Dim lnkManuf As New LinkButton
        Dim lnkPackg As New LinkButton
        Dim lnkSku As New LinkButton
        Dim lblProId As New Label
        Dim lblPackId As New Label
        Dim lblGraphId As New Label
        Dim lblStructId As New Label
        Dim lblFormulaId As New Label
        Dim lblManufId As New Label
        Dim lblPackgId As New Label
        Dim lblSkuId As New Label
        Dim lblProjID As New Label
        Dim lblActive As New Label
        Dim lblModelId As New Label
        Dim Chk As New CheckBox
        Try
            For Each Gr As GridViewRow In grdModel.Rows
                lblProId = grdModel.Rows(Gr.RowIndex).FindControl("lblProducerId")
                lblPackId = grdModel.Rows(Gr.RowIndex).FindControl("lblPackerId")
                lblGraphId = grdModel.Rows(Gr.RowIndex).FindControl("lblGraphicsId")
                lblStructId = grdModel.Rows(Gr.RowIndex).FindControl("lblStructId")
                lblFormulaId = grdModel.Rows(Gr.RowIndex).FindControl("lblFormulaId")
                lblManufId = grdModel.Rows(Gr.RowIndex).FindControl("lblManufId")
                lblPackgId = grdModel.Rows(Gr.RowIndex).FindControl("lblPackgId")
                lblSkuId = grdModel.Rows(Gr.RowIndex).FindControl("lblSkuId")

                lblProjID = grdModel.Rows(Gr.RowIndex).FindControl("lblProjectID")
                lblModelId = grdModel.Rows(Gr.RowIndex).FindControl("lblModelId")

                lnkProd = grdModel.Rows(Gr.RowIndex).FindControl("lnkProducer")
                lnkPack = grdModel.Rows(Gr.RowIndex).FindControl("lnkPacker")
                lnkGraph = grdModel.Rows(Gr.RowIndex).FindControl("lnkGraphics")
                lnkStruct = grdModel.Rows(Gr.RowIndex).FindControl("lnkStruct")
                lnkFormula = grdModel.Rows(Gr.RowIndex).FindControl("lnkFormula")
                lnkManuf = grdModel.Rows(Gr.RowIndex).FindControl("lnkManuf")
                lnkPackg = grdModel.Rows(Gr.RowIndex).FindControl("lnkPackg")
                lnkSku = grdModel.Rows(Gr.RowIndex).FindControl("lnkSku")
                lblActive = grdModel.Rows(Gr.RowIndex).FindControl("lblActive")
                Chk = Gr.FindControl("chkHide")

                For i = 3 To 17
                    Gr.Cells(i).Attributes.Add("style", "word-break:break-all;word-wrap:break-word;")
                Next

                If lblActive.Text = "N" Then
                    Gr.BackColor = Drawing.Color.LightGray
                    Gr.Attributes.Add("style", "color: rgb(100,100,100);")
                    lnkProd.CssClass = "SavvyGLink"
                    lnkPack.CssClass = "SavvyGLink"
                    lnkGraph.CssClass = "SavvyGLink"
                    lnkStruct.CssClass = "SavvyGLink"
                    lnkFormula.CssClass = "SavvyGLink"
                    lnkManuf.CssClass = "SavvyGLink"
                    lnkPackg.CssClass = "SavvyGLink"
                    lnkSku.CssClass = "SavvyGLink"
                    Gr.Cells(3).Enabled = False
                    Gr.Cells(4).Enabled = False
                    Gr.Cells(5).Enabled = False
                    Gr.Cells(6).Enabled = False
                    Gr.Cells(7).Enabled = False
                    Gr.Cells(8).Enabled = False
                    Gr.Cells(9).Enabled = False
                    Gr.Cells(10).Enabled = False
                    Gr.Cells(11).Enabled = False
                    Gr.Cells(12).Enabled = False
                    Gr.Cells(13).Enabled = False
                    Gr.Cells(14).Enabled = False
                    Gr.Cells(15).Enabled = False
                    Gr.Cells(16).Enabled = False
                    Gr.Cells(17).Enabled = False
                    Chk.Checked = True
                Else
                    lnkProd.Attributes.Add("onclick", "return ShowPopWindow('Popup/ViewProducerDetails.aspx?ProdId=" + lblProId.Text.Trim() + "&ProjId=" + lblProjID.Text.Trim() + "&ModelId=" + lblModelId.Text.Trim() + "');")
                    lnkPack.Attributes.Add("onclick", "return ShowPopWindow('Popup/ViewPackerDetails.aspx?PackId=" + lblPackId.Text.Trim() + "&ProjId=" + lblProjID.Text.Trim() + "&ModelId=" + lblModelId.Text.Trim() + "');")
                    lnkGraph.Attributes.Add("onclick", "return ShowPopWindow('Popup/ViewGraphicsDetails.aspx?GraphId=" + lblGraphId.Text.Trim() + "&ProjId=" + lblProjID.Text.Trim() + "&ModelId=" + lblModelId.Text.Trim() + "');")
                    lnkStruct.Attributes.Add("onclick", "return ShowNewPopWindow('Popup/ViewStructureDetails.aspx?StructureId=" + lblStructId.Text.Trim() + "&ProjId=" + lblProjID.Text.Trim() + "&ModelId=" + lblModelId.Text.Trim() + "');")
                    lnkFormula.Attributes.Add("onclick", "return ShowNewPopWindow('Popup/ViewFormulaDetails.aspx?FormulaId=" + lblFormulaId.Text.Trim() + "&ProjId=" + lblProjID.Text.Trim() + "&ModelId=" + lblModelId.Text.Trim() + "');")
                    lnkManuf.Attributes.Add("onclick", "return ShowNewPopWindow('Popup/ViewManufactoryDetails.aspx?ManufactoryId=" + lblManufId.Text.Trim() + "&ProjId=" + lblProjID.Text.Trim() + "&ModelId=" + lblModelId.Text.Trim() + "');")
                    lnkPackg.Attributes.Add("onclick", "return ShowNewPopWindow('Popup/ViewPackagingDetails.aspx?PackagingId=" + lblPackgId.Text.Trim() + "&ProjId=" + lblProjID.Text.Trim() + "&ModelId=" + lblModelId.Text.Trim() + "');")
                    lnkSku.Attributes.Add("onclick", "return ShowPopWindow('Popup/ViewSKUDetails.aspx?SKUId=" + lblSkuId.Text.Trim() + "&ProjId=" + lblProjID.Text.Trim() + "&ModelId=" + lblModelId.Text.Trim() + "');")

                    Gr.Cells(3).Enabled = True
                    Gr.Cells(4).Enabled = True
                    Gr.Cells(5).Enabled = True
                    Gr.Cells(6).Enabled = True
                    Gr.Cells(7).Enabled = True
                    Gr.Cells(8).Enabled = True
                    Gr.Cells(9).Enabled = True
                    Gr.Cells(10).Enabled = True
                    Gr.Cells(11).Enabled = True
                    Gr.Cells(12).Enabled = True
                    Gr.Cells(13).Enabled = True
                    Gr.Cells(14).Enabled = True
                    Gr.Cells(15).Enabled = True
                    Gr.Cells(16).Enabled = True
                    Gr.Cells(17).Enabled = True
                    Chk.Checked = False
                End If
            Next
        Catch ex As Exception
            Response.Write("Error:BindLink:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdModel_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdModel.PageIndexChanging
        Try
            grdModel.PageIndex = e.NewPageIndex
            BindModelGrid(grdModel.PageIndex)
            PageDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindModelGrid(ByVal index As Integer)
        Try
            Dim Dts As New DataSet
            Dim count As Integer = 0
            Dim Indcount As Integer = 0
            Dts = Session("ModelData")
            grdModel.DataSource = Dts
            grdModel.DataBind()
            If Dts.Tables(0).Rows.Count > 0 Then

                Dim dv As New DataView
                Dim dt As New DataTable
                dv = Dts.Tables(0).DefaultView()
                dv.RowFilter = "ISACTIVE='Y'"
                dt = dv.ToTable()
                lblCount.Text = "Number of models for the projects: " + dt.Rows.Count.ToString()

                count = Dts.Tables(0).Rows.Count
                'lblCount.Text = "Number of models for the projects: " + Dts.Tables(0).Rows.Count.ToString()
                divModel.Visible = True
                index = index + 1
                Indcount = index * 10
                If count < Indcount Then
                    If (count Mod 10) = 1 Then
                        divModel.Attributes.Add("style", "height: 113px; overflow: auto;  background-color: White;")
                    ElseIf (count Mod 10) = 2 Then
                        divModel.Attributes.Add("style", "height: 142px; overflow: auto;  background-color: White;")
                    ElseIf (count Mod 10) = 3 Then
                        divModel.Attributes.Add("style", "height: 170px; overflow: auto;  background-color: White;")
                    ElseIf (count Mod 10) = 4 Then
                        divModel.Attributes.Add("style", "height: 200px; overflow: auto;  background-color: White;")
                    ElseIf (count Mod 10) = 5 Then
                        divModel.Attributes.Add("style", "height: 225px; overflow: auto;  background-color: White;")
                    ElseIf (count Mod 10) = 6 Then
                        divModel.Attributes.Add("style", "height: 255px; overflow: auto;  background-color: White;")
                    ElseIf (count Mod 10) = 7 Then
                        divModel.Attributes.Add("style", "height: 280px; overflow: auto;  background-color: White;")
                    ElseIf (count Mod 10) = 8 Then
                        divModel.Attributes.Add("style", "height: 306px; overflow: auto;  background-color: White;")
                    ElseIf (count Mod 10) = 9 Then
                        divModel.Attributes.Add("style", "height: 337px; overflow: auto;  background-color: White;")
                    ElseIf (count Mod 10) = 0 Then
                        divModel.Attributes.Add("style", "height: 365px; overflow: auto;  background-color: White;")
                    End If
                Else
                    divModel.Attributes.Add("style", "height: 365px; overflow: auto;  background-color: White;")
                End If

                'divModel.Attributes.Add("height", "auto")
            Else
                divModel.Visible = False
            End If
            BindLink()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdModel_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdModel.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As New DataView
            Dim dt As New DataTable
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidSortId.Value.ToString())
            Dts = Session("ModelData")
            Dts.Tables(0).DefaultView.RowFilter = ""
            dv = Dts.Tables(0).DefaultView


            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortId.Value = numberDiv.ToString()
            grdModel.DataSource = dv

            If Dts.Tables(0).Rows.Count > 0 Then
                lblCount.Text = "Number of models for the projects: " + Dts.Tables(0).Rows.Count.ToString()
                divModel.Visible = True
                If Dts.Tables(0).Rows.Count = 1 Then
                    divModel.Attributes.Add("style", "height: 113px; overflow: auto;  background-color: White;")
                ElseIf Dts.Tables(0).Rows.Count = 2 Then
                    divModel.Attributes.Add("style", "height: 142px; overflow: auto;  background-color: White;")
                ElseIf Dts.Tables(0).Rows.Count = 3 Then
                    divModel.Attributes.Add("style", "height: 170px; overflow: auto;  background-color: White;")
                ElseIf Dts.Tables(0).Rows.Count = 4 Then
                    divModel.Attributes.Add("style", "height: 200px; overflow: auto;  background-color: White;")
                ElseIf Dts.Tables(0).Rows.Count = 5 Then
                    divModel.Attributes.Add("style", "height: 225px; overflow: auto;  background-color: White;")
                ElseIf Dts.Tables(0).Rows.Count = 6 Then
                    divModel.Attributes.Add("style", "height: 255px; overflow: auto;  background-color: White;")
                ElseIf Dts.Tables(0).Rows.Count = 7 Then
                    divModel.Attributes.Add("style", "height: 280px; overflow: auto;  background-color: White;")
                ElseIf Dts.Tables(0).Rows.Count = 8 Then
                    divModel.Attributes.Add("style", "height: 306px; overflow: auto;  background-color: White;")
                ElseIf Dts.Tables(0).Rows.Count = 9 Then
                    divModel.Attributes.Add("style", "height: 337px; overflow: auto;  background-color: White;")
                ElseIf Dts.Tables(0).Rows.Count > 10 Then
                    divModel.Attributes.Add("style", "height: 365px; overflow: auto;  background-color: White;")
                End If
                'divModel.Attributes.Add("height", "auto")
            Else
                divModel.Visible = False
            End If
            grdModel.DataBind()

            dv.RowFilter = "ISACTIVE='Y'"
            dt = dv.ToTable()
            lblCount.Text = "Number of models for the projects: " + dt.Rows.Count.ToString()

            BindLink()

            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ProjectManager.aspx", "Sorted Project Report Grid by Expression:" + e.SortExpression, Session("ProjectId"), Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

        Catch ex As Exception

        End Try
    End Sub

#Region "Add Click event"

    Protected Sub btnProd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProd.Click
        Dim objUpIns As New UpdateInsert()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim seq As Integer = 0
        Try
            If txtProd.Text <> "" Then
                ds = objGetData.GetModelValueDetails(Session("ProjectId"), "4", txtProd.Text.Replace("'", "''"))
                If ds.Tables(0).Rows.Count = 0 Then
                    seq = objUpIns.InsertProjCategoryDetails(Session("ProjectId"), "4", txtProd.Text.Replace("'", "''"))
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Added new Product in Project, Product:" + txtProd.Text.Replace("'", "''") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                        Dim ht As New Hashtable
                        If Session("ht") IsNot Nothing Then
                            ht = DirectCast(Session("ht"), Hashtable)
                        End If
                        Dim str() As String
                        For i = 1 To ht.Count
                            For Each item As Object In ht
                                str = item.Key.ToString().Split("-")
                                If str(1) = i Then
                                    objUpIns.EditInsertLog(Session("UserId").ToString(), "5", str(0) + " " + seq.ToString(), seq.ToString(), item.Value.ToString().Replace("'", "''"), hidProjectId.Value, Session("SPROJLogInCount").ToString())
                                    Exit For
                                End If
                            Next
                        Next
                        Session("ht") = Nothing
                        Session("SeqCnt") = "1"
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes

                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Product to Pack " + txtProd.Text + " already exist.');", True)
                End If

            End If

            txtProd.Text = ""
            GetProductDetails()

        Catch ex As Exception
            lblError.Text = "Error:btnProd_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSize.Click
        Dim objUpIns As New UpdateInsert()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim seq As Integer = 0
        Try
            If txtSize.Text <> "" Then
                ds = objGetData.GetModelValueDetails(Session("ProjectId"), "3", txtSize.Text.Replace("'", "''"))
                If ds.Tables(0).Rows.Count = 0 Then
                    seq = objUpIns.InsertProjCategoryDetails(Session("ProjectId"), "3", txtSize.Text.Replace("'", "''"))
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Added new Package Size in Project, Package Size:" + txtSize.Text.Replace("'", "''") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                        Dim ht As New Hashtable
                        If Session("ht") IsNot Nothing Then
                            ht = DirectCast(Session("ht"), Hashtable)
                        End If
                        Dim str() As String
                        For i = 1 To ht.Count
                            For Each item As Object In ht
                                str = item.Key.ToString().Split("-")
                                If str(1) = i Then
                                    objUpIns.EditInsertLog(Session("UserId").ToString(), "5", str(0) + " " + seq.ToString(), seq.ToString(), item.Value.ToString().Replace("'", "''"), hidProjectId.Value, Session("SPROJLogInCount").ToString())
                                    Exit For
                                End If
                            Next
                        Next
                        Session("ht") = Nothing
                        Session("SeqCnt") = "1"
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes

                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Package Size " + txtSize.Text + " already exist.');", True)
                End If

            End If

            txtSize.Text = ""
            GetSizeDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnSize_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnType.Click
        Dim objUpIns As New UpdateInsert()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim seq As Integer = 0
        Try
            If txtType.Text <> "" Then
                ds = objGetData.GetModelValueDetails(Session("ProjectId"), "1", txtType.Text.Replace("'", "''"))
                If ds.Tables(0).Rows.Count = 0 Then
                    seq = objUpIns.InsertProjCategoryDetails(Session("ProjectId"), "1", txtType.Text.Replace("'", "''"))
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Added new Package Type in Project, Package Type:" + txtType.Text.Replace("'", "''") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                        Dim ht As New Hashtable
                        If Session("ht") IsNot Nothing Then
                            ht = DirectCast(Session("ht"), Hashtable)
                        End If
                        Dim str() As String
                        For i = 1 To ht.Count
                            For Each item As Object In ht
                                str = item.Key.ToString().Split("-")
                                If str(1) = i Then
                                    objUpIns.EditInsertLog(Session("UserId").ToString(), "5", str(0) + " " + seq.ToString(), seq.ToString(), item.Value.ToString().Replace("'", "''"), hidProjectId.Value, Session("SPROJLogInCount").ToString())
                                    Exit For
                                End If
                            Next
                        Next
                        Session("ht") = Nothing
                        Session("SeqCnt") = "1"
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes

                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Package Type " + txtType.Text + " already exist.');", True)
                End If


            End If

            txtType.Text = ""
            GetTypeDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnType_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnGeog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGeog.Click
        Dim objUpIns As New UpdateInsert()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim seq As Integer = 0
        Try
            If txtGeog.Text <> "" Then
                ds = objGetData.GetModelValueDetails(Session("ProjectId"), "5", txtGeog.Text.Replace("'", "''"))
                If ds.Tables(0).Rows.Count = 0 Then
                    seq = objUpIns.InsertProjCategoryDetails(Session("ProjectId"), "5", txtGeog.Text.Replace("'", "''"))
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Added new Geography in Project, Geography:" + txtGeog.Text.Replace("'", "''") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                        Dim ht As New Hashtable
                        If Session("ht") IsNot Nothing Then
                            ht = DirectCast(Session("ht"), Hashtable)
                        End If
                        Dim str() As String
                        For i = 1 To ht.Count
                            For Each item As Object In ht
                                str = item.Key.ToString().Split("-")
                                If str(1) = i Then
                                    objUpIns.EditInsertLog(Session("UserId").ToString(), "5", str(0) + " " + seq.ToString(), seq.ToString(), item.Value.ToString().Replace("'", "''"), hidProjectId.Value, Session("SPROJLogInCount").ToString())
                                    Exit For
                                End If
                            Next
                        Next
                        Session("ht") = Nothing
                        Session("SeqCnt") = "1"
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes

                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Geography " + txtGeog.Text + " already exist.');", True)
                End If
            End If

            txtGeog.Text = ""
            GetGeographyDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnGeog_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnChain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChain.Click
        Dim objUpIns As New UpdateInsert()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim seq As Integer = 0
        Try
            If txtChain.Text <> "" Then
                ds = objGetData.GetModelValueDetails(Session("ProjectId"), "2", txtChain.Text.Replace("'", "''"))
                If ds.Tables(0).Rows.Count = 0 Then
                    seq = objUpIns.InsertProjCategoryDetails(Session("ProjectId"), "2", txtChain.Text.Replace("'", "''"))
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Added new Value Chain in Project, Value Chain:" + txtChain.Text.Replace("'", "''") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                        Dim ht As New Hashtable
                        If Session("ht") IsNot Nothing Then
                            ht = DirectCast(Session("ht"), Hashtable)
                        End If
                        Dim str() As String
                        For i = 1 To ht.Count
                            For Each item As Object In ht
                                str = item.Key.ToString().Split("-")
                                If str(1) = i Then
                                    objUpIns.EditInsertLog(Session("UserId").ToString(), "5", str(0) + " " + seq.ToString(), seq.ToString(), item.Value.ToString().Replace("'", "''"), hidProjectId.Value, Session("SPROJLogInCount").ToString())
                                    Exit For
                                End If
                            Next
                        Next
                        Session("ht") = Nothing
                        Session("SeqCnt") = "1"
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes

                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Value Chain " + txtChain.Text + " already exist.');", True)
                End If
            End If

            txtChain.Text = ""
            GetChainDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnChain_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnFeat1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFeat1.Click
        Dim objUpIns As New UpdateInsert()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim seq As Integer = 0
        Try
            If txtFeat1.Text <> "" Then
                ds = objGetData.GetModelValueDetails(Session("ProjectId"), "6", txtFeat1.Text.Replace("'", "''"))
                If ds.Tables(0).Rows.Count = 0 Then
                    seq = objUpIns.InsertProjCategoryDetails(Session("ProjectId"), "6", txtFeat1.Text.Replace("'", "''"))
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Added new Special Features 1 in Project, Special Features 1:" + txtFeat1.Text.Replace("'", "''") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                        Dim ht As New Hashtable
                        If Session("ht") IsNot Nothing Then
                            ht = DirectCast(Session("ht"), Hashtable)
                        End If
                        Dim str() As String
                        For i = 1 To ht.Count
                            For Each item As Object In ht
                                str = item.Key.ToString().Split("-")
                                If str(1) = i Then
                                    objUpIns.EditInsertLog(Session("UserId").ToString(), "5", str(0) + " " + seq.ToString(), seq.ToString(), item.Value.ToString().Replace("'", "''"), hidProjectId.Value, Session("SPROJLogInCount").ToString())
                                    Exit For
                                End If
                            Next
                        Next
                        Session("ht") = Nothing
                        Session("SeqCnt") = "1"
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes

                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Special Features 1 " + txtFeat1.Text + " already exist.');", True)
                End If
            End If

            txtFeat1.Text = ""
            GetFeat1Details()
        Catch ex As Exception
            lblError.Text = "Error:btnFeat1_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnFeat2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFeat2.Click
        Dim objUpIns As New UpdateInsert()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim seq As Integer = 0
        Try
            If txtFeat2.Text <> "" Then
                ds = objGetData.GetModelValueDetails(Session("ProjectId"), "7", txtFeat2.Text.Replace("'", "''"))
                If ds.Tables(0).Rows.Count = 0 Then
                    seq = objUpIns.InsertProjCategoryDetails(Session("ProjectId"), "7", txtFeat2.Text.Replace("'", "''"))
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Added new Special Features 2 in Project, Special Features 2:" + txtFeat2.Text.Replace("'", "''") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())
                        Dim ht As New Hashtable
                        If Session("ht") IsNot Nothing Then
                            ht = DirectCast(Session("ht"), Hashtable)
                        End If
                        Dim str() As String
                        For i = 1 To ht.Count
                            For Each item As Object In ht
                                str = item.Key.ToString().Split("-")
                                If str(1) = i Then
                                    objUpIns.EditInsertLog(Session("UserId").ToString(), "5", str(0) + " " + seq.ToString(), seq.ToString(), item.Value.ToString().Replace("'", "''"), hidProjectId.Value, Session("SPROJLogInCount").ToString())
                                    Exit For
                                End If
                            Next
                        Next
                        Session("ht") = Nothing
                        Session("SeqCnt") = "1"
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes

                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Special Features 2 " + txtFeat2.Text + " already exist.');", True)
                End If
            End If

            txtFeat2.Text = ""
            GetFeat2Details()
        Catch ex As Exception
            lblError.Text = "Error:btnFeat2_Click:" + ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "Bind Checkbox Table"
    Protected Sub GetProductDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblProd.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "PROD")
            ProdCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trProd.Visible = True
            Else
                trProd.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        txtProd.Enabled = False
                        btnProd.Enabled = False
                        btnEProd.Enabled = False
                    End If
                End If

            End If

            For i = 1 To ds.Tables(0).Rows.Count

                tdInner = New TableCell
                If i Mod 2 = 1 Then
                    trInner = New TableRow
                End If

                chkbx = New CheckBox
                chkbx.ID = "chkBxProd" + i.ToString()
                chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()

                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                chkbx.Width = "260"

                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        txtProd.Enabled = False
                        btnProd.Enabled = False
                        btnEProd.Enabled = False
                    End If
                End If

                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblProd.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetProductDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSizeDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblSize.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "SIZE")
            SizeCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trSize.Visible = True
            Else
                trSize.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        txtSize.Enabled = False
                        btnSize.Enabled = False
                        btnESize.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                If i Mod 2 = 1 Then
                    trInner = New TableRow
                End If
                tdInner = New TableCell

                chkbx = New CheckBox
                chkbx.ID = "chkBxSize" + i.ToString()
                chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                chkbx.Width = "260"
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        txtSize.Enabled = False
                        btnSize.Enabled = False
                        btnESize.Enabled = False
                    End If
                End If
                'chkbx.AutoPostBack = True
                'AddHandler chkbx.CheckedChanged, AddressOf chkbxSize_CheckedChanged
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblSize.Controls.Add(trInner)
            Next
        Catch ex As Exception
            ErrorLable.Text = "Error:GetSizeDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetTypeDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblType.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "TYPE")
            TypeCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trType.Visible = True
            Else
                trType.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        txtType.Enabled = False
                        btnType.Enabled = False
                        btnEType.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                If i Mod 2 = 1 Then
                    trInner = New TableRow
                End If
                tdInner = New TableCell

                chkbx = New CheckBox
                chkbx.ID = "chkBxType" + i.ToString()
                chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True
                'AddHandler chkbx.CheckedChanged, AddressOf chkbxSize_CheckedChanged
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        txtType.Enabled = False
                        btnType.Enabled = False
                        btnEType.Enabled = False
                    End If
                End If
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblType.Controls.Add(trInner)
            Next
        Catch ex As Exception
            ErrorLable.Text = "Error:GetTypeDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetGeographyDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblGeog.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "GEOG")
            GeogCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trGeog.Visible = True
            Else
                trGeog.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        txtGeog.Enabled = False
                        btnGeog.Enabled = False
                        btnEGeog.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                If i Mod 2 = 1 Then
                    trInner = New TableRow
                End If
                tdInner = New TableCell

                chkbx = New CheckBox
                chkbx.ID = "chkBxGeog" + i.ToString()
                chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                chkbx.Width = "260"
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        txtGeog.Enabled = False
                        btnGeog.Enabled = False
                        btnEGeog.Enabled = False
                    End If
                End If
                'chkbx.AutoPostBack = True
                'AddHandler chkbx.CheckedChanged, AddressOf chkbxSize_CheckedChanged
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblGeog.Controls.Add(trInner)
            Next
        Catch ex As Exception
            ErrorLable.Text = "Error:GetGeographyDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetChainDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblChain.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "VCHAIN")
            ChainCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trChain.Visible = True
            Else
                trChain.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        txtChain.Enabled = False
                        btnChain.Enabled = False
                        btnEChain.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                If i Mod 2 = 1 Then
                    trInner = New TableRow
                End If
                tdInner = New TableCell

                chkbx = New CheckBox
                chkbx.ID = "chkBxChain" + i.ToString()
                chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True
                'AddHandler chkbx.CheckedChanged, AddressOf chkbxSize_CheckedChanged
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        txtChain.Enabled = False
                        btnChain.Enabled = False
                        btnEChain.Enabled = False
                    End If
                End If
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblChain.Controls.Add(trInner)
            Next
        Catch ex As Exception
            ErrorLable.Text = "Error:GetChainDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetFeat1Details()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblFeat1.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "SPFET1")
            Feat1Count = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trFeat1.Visible = True
            Else
                trFeat1.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        txtFeat1.Enabled = False
                        btnFeat1.Enabled = False
                        btnEFeat1.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                If i Mod 2 = 1 Then
                    trInner = New TableRow
                End If
                tdInner = New TableCell

                chkbx = New CheckBox
                chkbx.ID = "chkBxFeat1" + i.ToString()
                chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True
                'AddHandler chkbx.CheckedChanged, AddressOf chkbxSize_CheckedChanged
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        txtFeat1.Enabled = False
                        btnFeat1.Enabled = False
                        btnEFeat1.Enabled = False
                    End If
                End If
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblFeat1.Controls.Add(trInner)
            Next
        Catch ex As Exception
            ErrorLable.Text = "Error:GetFeat1Details:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetFeat2Details()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblFeat2.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "SPFET2")
            Feat2Count = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trFeat2.Visible = True
            Else
                trFeat2.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        txtFeat2.Enabled = False
                        btnFeat2.Enabled = False
                        btnEFeat2.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                If i Mod 2 = 1 Then
                    trInner = New TableRow
                End If
                tdInner = New TableCell

                chkbx = New CheckBox
                chkbx.ID = "chkBxFeat2" + i.ToString()
                chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True
                'AddHandler chkbx.CheckedChanged, AddressOf chkbxSize_CheckedChanged
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        txtFeat2.Enabled = False
                        btnFeat2.Enabled = False
                        btnEFeat2.Enabled = False
                    End If
                End If
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblFeat2.Controls.Add(trInner)
            Next
        Catch ex As Exception
            ErrorLable.Text = "Error:GetFeat2Details:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetProducerDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblProducer.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "PRODUCER")
            ProduCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trProducer.Visible = True
            Else
                trProducer.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        lnkProducer.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                tdInner = New TableCell

                trInner = New TableRow


                chkbx = New CheckBox
                chkbx.ID = "chkBxProdu" + i.ToString()
                'chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()

                chkbx.Text = "<a href='#' onclick='return ShowPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("PRODUCERID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",1,0)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        lnkProducer.Enabled = False
                        chkbx.Text = "<a href='#' onclick='return ShowPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("PRODUCERID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",1,1)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"

                    End If
                End If
                'chkbx.Text = "<a href='#' onclick='return ShowPopWindow('Popup/ViewProducerDetails.aspx');'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>" 'ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True

                'AddHandler chkbx.CheckedChanged, AddressOf chkbxProd_CheckedChanged
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblProducer.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetProductDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPackerDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblPacker.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "PACKER")
            PackCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trPacker.Visible = True
            Else
                trPacker.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        lnkPacker.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                tdInner = New TableCell

                trInner = New TableRow


                chkbx = New CheckBox
                chkbx.ID = "chkBxPack" + i.ToString()
                'chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                chkbx.Text = "<a href='#' onclick='return ShowPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("PACKERID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",2,0)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        lnkPacker.Enabled = False
                        chkbx.Text = "<a href='#' onclick='return ShowPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("PACKERID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",2,1)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"

                    End If
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True

                'AddHandler chkbx.CheckedChanged, AddressOf chkbxProd_CheckedChanged
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblPacker.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetProductDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetGraphicDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblGraphics.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "GRAPHIC")
            GraphCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trGraphics.Visible = True
            Else
                trGraphics.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        lnkGraphics.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                tdInner = New TableCell

                trInner = New TableRow


                chkbx = New CheckBox
                chkbx.ID = "chkBxGraph" + i.ToString()
                ' chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                chkbx.Text = "<a href='#' onclick='return ShowPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("GRAPHICSID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",3,0)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        lnkGraphics.Enabled = False
                        chkbx.Text = "<a href='#' onclick='return ShowPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("GRAPHICSID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",3,1)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                    End If
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True

                'AddHandler chkbx.CheckedChanged, AddressOf chkbxProd_CheckedChanged
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblGraphics.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetProductDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetStructureDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblStruct.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "STRUCT")
            StructCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trStruct.Visible = True
            Else
                trStruct.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        lnkStruct.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                tdInner = New TableCell

                trInner = New TableRow


                chkbx = New CheckBox
                chkbx.ID = "chkBxStruct" + i.ToString()
                ' chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                chkbx.Text = "<a href='#' onclick='return ShowNewPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("STRUCTUREID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",4,0)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        lnkStruct.Enabled = False
                        chkbx.Text = "<a href='#' onclick='return ShowNewPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("STRUCTUREID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",4,1)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                    End If
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True

                'AddHandler chkbx.CheckedChanged, AddressOf chkbxProd_CheckedChanged
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblStruct.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetStructureDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetFormulaDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblFormula.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "FORMULA")
            FormCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trFormula.Visible = True
            Else
                trFormula.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        lnkFormula.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                tdInner = New TableCell

                trInner = New TableRow


                chkbx = New CheckBox
                chkbx.ID = "chkBxFormula" + i.ToString()
                ' chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                chkbx.Text = "<a href='#' onclick='return ShowNewPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("FORMULAID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",5,0)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If

                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        lnkFormula.Enabled = False
                        chkbx.Text = "<a href='#' onclick='return ShowNewPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("FORMULAID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",5,1)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                    End If
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True

                'AddHandler chkbx.CheckedChanged, AddressOf chkbxProd_CheckedChanged
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblFormula.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetFormulaDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetManufDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblManuf.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "MANUF")
            ManufCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trManuf.Visible = True
            Else
                trManuf.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        lnkManuf.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                tdInner = New TableCell

                trInner = New TableRow


                chkbx = New CheckBox
                chkbx.ID = "chkBxManuf" + i.ToString()
                ' chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                chkbx.Text = "<a href='#' onclick='return ShowManuPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("MANUFACTORYID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",6,0)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        lnkManuf.Enabled = False
                        chkbx.Text = "<a href='#' onclick='return ShowManuPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("MANUFACTORYID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",6,1)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                    End If
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True

                'AddHandler chkbx.CheckedChanged, AddressOf chkbxProd_CheckedChanged
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblManuf.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetManufDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPackgDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblPack.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "PACKG")
            PackgCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trPack.Visible = True
            Else
                trPack.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        lnkPackg.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                tdInner = New TableCell

                trInner = New TableRow


                chkbx = New CheckBox
                chkbx.ID = "chkBxPackg" + i.ToString()
                ' chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                chkbx.Text = "<a href='#' onclick='return ShowManuPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("PACKAGINGID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",7,0)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        lnkPackg.Enabled = False
                        chkbx.Text = "<a href='#' onclick='return ShowManuPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("PACKAGINGID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",7,1)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                    End If
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True

                'AddHandler chkbx.CheckedChanged, AddressOf chkbxProd_CheckedChanged
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblPack.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetManufDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSkuDetails()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim chkbx As New CheckBox
        Dim tdInner As TableCell
        Dim trInner As New TableRow
        Try
            tblSKU.Rows.Clear()
            ds = objGetData.GetProductDetails(Session("ProjectId"), "SKU")
            SkuCount = ds.Tables(0).Rows.Count
            If ds.Tables(0).Rows.Count > 0 Then
                trSKU.Visible = True
            Else
                trSKU.Visible = False
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        lnkSKU.Enabled = False
                    End If
                End If
            End If

            For i = 1 To ds.Tables(0).Rows.Count

                tdInner = New TableCell

                trInner = New TableRow


                chkbx = New CheckBox
                chkbx.ID = "chkBxSku" + i.ToString()
                ' chkbx.Text = ds.Tables(0).Rows(i - 1).Item("VALUE").ToString()
                chkbx.Text = "<a href='#' onclick='return ShowPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("SKUID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",8,0)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                If ds.Tables(0).Rows(i - 1).Item("ISACTIVE").ToString() = "Y" Then
                    chkbx.Checked = True
                Else
                    chkbx.Checked = False
                End If
                If Session("SavvyAnalyst") <> "Y" Then
                    If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                        chkbx.Enabled = False
                        lnkSKU.Enabled = False
                        chkbx.Text = "<a href='#' onclick='return ShowPopWindowProd(" + ds.Tables(0).Rows(i - 1).Item("SKUID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJECTID").ToString() + "," + ds.Tables(0).Rows(i - 1).Item("PROJCATEGORYID").ToString() + ",8,1)'>" + ds.Tables(0).Rows(i - 1).Item("VALUE").ToString() + "</a>"
                    End If
                End If
                chkbx.Width = "260"
                'chkbx.AutoPostBack = True

                'AddHandler chkbx.CheckedChanged, AddressOf chkbxProd_CheckedChanged
                tdInner.Controls.Add(chkbx)
                trInner.Controls.Add(tdInner)
                tblSKU.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetManufDetails:" + ex.Message.ToString()
        End Try
    End Sub
#End Region

    Protected Sub btnGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrid.Click
        Dim ds As New DataSet
        Dim dsModel As New DataSet
        Dim dvProd As New DataView
        Dim dvSize As New DataView
        Dim dvType As New DataView
        Dim dvGeog As New DataView
        Dim dvChain As New DataView
        Dim dvFeat1 As New DataView
        Dim dvFeat2 As New DataView
        Dim dvProdu As New DataView
        Dim dvPack As New DataView
        Dim dvGraph As New DataView
        Dim dvStruct As New DataView
        Dim dvForm As New DataView
        Dim dvManuf As New DataView
        Dim dvPackg As New DataView
        Dim dvSku As New DataView
        Dim dtProd As New DataTable
        Dim dtSize As New DataTable
        Dim dtType As New DataTable
        Dim dtGeog As New DataTable
        Dim dtChain As New DataTable
        Dim dtFeat1 As New DataTable
        Dim dtFeat2 As New DataTable
        Dim dtProdu As New DataTable
        Dim dtPack As New DataTable
        Dim dtGraph As New DataTable
        Dim dtStruct As New DataTable
        Dim dtForm As New DataTable
        Dim dtManuf As New DataTable
        Dim dtPackg As New DataTable
        Dim dtSku As New DataTable
        Dim objGetData As New Selectdata()
        Dim objUpIns As New UpdateInsert()
        Dim strProd As String = String.Empty
        Dim chkbx As CheckBox
        Try

            For j = 1 To ProdCount
                Dim cntl As Control = FindControl("chkBxProd" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "4", chkbx.Text.Replace("'", "''"), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("PRODPACK").ToString() = chkbx.Text Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "PROD", chkbx.Text.Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To SizeCount
                Dim cntl As Control = FindControl("chkBxSize" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "3", chkbx.Text.Replace("'", "''"), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("PACKSIZE").ToString() = chkbx.Text Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "SIZE", chkbx.Text.Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To TypeCount
                Dim cntl As Control = FindControl("chkBxType" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "1", chkbx.Text.Replace("'", "''"), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("PACKTYPE").ToString() = chkbx.Text Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "TYPE", chkbx.Text.Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To GeogCount
                Dim cntl As Control = FindControl("chkBxGeog" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "5", chkbx.Text.Replace("'", "''"), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString() = chkbx.Text Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "GEOG", chkbx.Text.Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To ChainCount
                Dim cntl As Control = FindControl("chkBxChain" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "2", chkbx.Text.Replace("'", "''"), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("VCHAIN").ToString() = chkbx.Text Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "VCHAIN", chkbx.Text.Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To Feat1Count
                Dim cntl As Control = FindControl("chkBxFeat1" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "6", chkbx.Text.Replace("'", "''"), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("SPFET1").ToString() = chkbx.Text Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "SPFET1", chkbx.Text.Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To Feat2Count
                Dim cntl As Control = FindControl("chkBxFeat2" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "7", chkbx.Text.Replace("'", "''"), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("SPFET2").ToString() = chkbx.Text Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "SPFET2", chkbx.Text.Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To ProduCount
                Dim cntl As Control = FindControl("chkBxProdu" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                Dim strP() As String = Regex.Split(chkbx.Text, ">")
                Dim strT() As String = Regex.Split(strP(1), "<")

                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "8", strT(0), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("PRODUCER").ToString() = strT(0) Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "PRODUC", strT(0).Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To PackCount
                Dim cntl As Control = FindControl("chkBxPack" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                Dim strP() As String = Regex.Split(chkbx.Text, ">")
                Dim strT() As String = Regex.Split(strP(1), "<")
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "9", strT(0), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("PACKER").ToString() = strT(0) Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "PACKER", strT(0).Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To GraphCount
                Dim cntl As Control = FindControl("chkBxGraph" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                Dim strG() As String = Regex.Split(chkbx.Text, ">")
                Dim strT() As String = Regex.Split(strG(1), "<")
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "10", strT(0), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("GRAPHICS").ToString() = strT(0) Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "GRAPH", strT(0).Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To StructCount
                Dim cntl As Control = FindControl("chkBxStruct" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                Dim strS() As String = Regex.Split(chkbx.Text, ">")
                Dim strT() As String = Regex.Split(strS(1), "<")
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "11", strT(0), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("STRUCTURES").ToString() = strT(0) Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "STRUCT", strT(0).Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To FormCount
                Dim cntl As Control = FindControl("chkBxFormula" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                Dim strF() As String = Regex.Split(chkbx.Text, ">")
                Dim strT() As String = Regex.Split(strF(1), "<")
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "12", strT(0), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("FORMULA").ToString() = strT(0) Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "FORMULA", strT(0).Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To ManufCount
                Dim cntl As Control = FindControl("chkBxManuf" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                Dim strM() As String = Regex.Split(chkbx.Text, ">")
                Dim strT() As String = Regex.Split(strM(1), "<")
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "13", strT(0), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString() = strT(0) Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "MANUF", strT(0).Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To PackgCount
                Dim cntl As Control = FindControl("chkBxPackg" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                Dim strPg() As String = Regex.Split(chkbx.Text, ">")
                Dim strT() As String = Regex.Split(strPg(1), "<")
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "14", strT(0), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("PACKAGING").ToString() = strT(0) Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "PACKG", strT(0).Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To SkuCount
                Dim cntl As Control = FindControl("chkBxSku" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                Dim strSk() As String = Regex.Split(chkbx.Text, ">")
                Dim strT() As String = Regex.Split(strSk(1), "<")
                If Not chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "15", strT(0), "N")
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("SKU").ToString() = strT(0) Then
                                objUpIns.DeleteModelByType(Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString(), "SKU", strT(0).Replace("'", "''"))
                            End If
                        Next
                    End If
                End If
            Next

            For j = 1 To ProdCount
                Dim cntl As Control = FindControl("chkBxProd" + j.ToString())
                chkbx = CType(cntl, CheckBox)

                If chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "4", chkbx.Text.Replace("'", "''"), "Y")

                    'dsModel = objGetData.GetModelDetails(Session("ProjectId"), "PROD", chkbx.Text)

                    'If dsModel.Tables(0).Rows.Count < 1 Then
                    'Insert Product to Pack Data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        dvProd = ds.Tables(0).DefaultView()
                        dvProd.RowFilter = "PRODPACK IS NULL"
                        dtProd = dvProd.ToTable()
                        If dtProd.Rows.Count > 0 Then
                            For i = 0 To dtProd.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtProd.Rows(i).Item("MODELID").ToString(), chkbx.Text.Replace("'", "''"), dtProd.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtProd.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtProd.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtProd.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtProd.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtProd.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                          dtProd.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtProd.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtProd.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtProd.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtProd.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtProd.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtProd.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtProd.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("PRODPACK").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), chkbx.Text.Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                 ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), chkbx.Text.Replace("'", "''"), "", "", "", "", "", "", "", "", "", "", "", "", "", "")
                    End If
                    'End If                
                End If
            Next

            For j = 1 To SizeCount
                Dim cntl As Control = FindControl("chkBxSize" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "3", chkbx.Text.Replace("'", "''"), "Y")

                    'dsModel = objGetData.GetModelDetails(Session("ProjectId"), "SIZE", chkbx.Text)

                    'If dsModel.Tables(0).Rows.Count < 1 Then
                    'Insert Package Size Data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        dvSize = ds.Tables(0).DefaultView()
                        dvSize.RowFilter = "PACKSIZE IS NULL"
                        dtSize = dvSize.ToTable()

                        If dtSize.Rows.Count > 0 Then
                            For i = 0 To dtSize.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtSize.Rows(i).Item("MODELID").ToString(), dtSize.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), dtSize.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtSize.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtSize.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtSize.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), _
                                                          dtSize.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), dtSize.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtSize.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtSize.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtSize.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtSize.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtSize.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtSize.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtSize.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("PACKSIZE").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                 ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If

                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", chkbx.Text.Replace("'", "''"), "", "", "", "", "", "", "", "", "", "", "", "", "")
                    End If
                    'End If                
                End If
            Next

            For j = 1 To TypeCount
                Dim cntl As Control = FindControl("chkBxType" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "1", chkbx.Text.Replace("'", "''"), "Y")

                    'dsModel = objGetData.GetModelDetails(Session("ProjectId"), "TYPE", chkbx.Text)

                    'If dsModel.Tables(0).Rows.Count < 1 Then
                    'Insert Package Type data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        dvType = ds.Tables(0).DefaultView()
                        dvType.RowFilter = "PACKTYPE IS NULL"
                        dtType = dvType.ToTable()

                        If dtType.Rows.Count > 0 Then
                            For i = 0 To dtType.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtType.Rows(i).Item("MODELID").ToString(), dtType.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtType.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), dtType.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtType.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtType.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtType.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                          dtType.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtType.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtType.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtType.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtType.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtType.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtType.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtType.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("PACKTYPE").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                 ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If

                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", chkbx.Text.Replace("'", "''"), "", "", "", "", "", "", "", "", "", "", "", "")
                    End If
                End If
            Next

            For j = 1 To GeogCount
                Dim cntl As Control = FindControl("chkBxGeog" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "5", chkbx.Text.Replace("'", "''"), "Y")

                    'Insert Geography data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvGeog = ds.Tables(0).DefaultView()
                        dvGeog.RowFilter = "GEOGRAPHY IS NULL"
                        dtGeog = dvGeog.ToTable()

                        If dtGeog.Rows.Count > 0 Then
                            For i = 0 To dtGeog.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtGeog.Rows(i).Item("MODELID").ToString(), dtGeog.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), dtGeog.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                          dtGeog.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtGeog.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                        ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If

                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", chkbx.Text.Replace("'", "''"), "", "", "", "", "", "", "", "", "", "", "")
                    End If

                End If
            Next

            For j = 1 To ChainCount
                Dim cntl As Control = FindControl("chkBxChain" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "2", chkbx.Text.Replace("'", "''"), "Y")

                    'Insert Value Chain data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvChain = ds.Tables(0).DefaultView()
                        dvChain.RowFilter = "VCHAIN IS NULL"
                        dtChain = dvChain.ToTable()

                        If dtChain.Rows.Count > 0 Then
                            For i = 0 To dtChain.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtChain.Rows(i).Item("MODELID").ToString(), dtChain.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtChain.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtChain.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtChain.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), dtChain.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtChain.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                          dtChain.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtChain.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtChain.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtChain.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtChain.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtChain.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtChain.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtChain.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("VCHAIN").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                        ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", chkbx.Text.Replace("'", "''"), "", "", "", "", "", "", "", "", "", "")
                    End If

                End If
            Next

            For j = 1 To Feat1Count
                Dim cntl As Control = FindControl("chkBxFeat1" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "6", chkbx.Text.Replace("'", "''"), "Y")

                    'Insert Special Features 1 data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        dvFeat1 = ds.Tables(0).DefaultView()
                        dvFeat1.RowFilter = "SPFET1 IS NULL"
                        dtFeat1 = dvFeat1.ToTable()

                        If dtFeat1.Rows.Count > 0 Then
                            For i = 0 To dtFeat1.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtFeat1.Rows(i).Item("MODELID").ToString(), dtFeat1.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), dtFeat1.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                          dtFeat1.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtFeat1.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("SPFET1").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                        ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", "", chkbx.Text.Replace("'", "''"), "", "", "", "", "", "", "", "", "")
                    End If

                End If
            Next

            For j = 1 To Feat2Count
                Dim cntl As Control = FindControl("chkBxFeat2" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "7", chkbx.Text.Replace("'", "''"), "Y")

                    'Insert Special Features 1 data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvFeat2 = ds.Tables(0).DefaultView()
                        dvFeat2.RowFilter = "SPFET2 IS NULL"
                        dtFeat2 = dvFeat2.ToTable()

                        If dtFeat2.Rows.Count > 0 Then
                            For i = 0 To dtFeat2.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtFeat2.Rows(i).Item("MODELID").ToString(), dtFeat2.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), _
                                                          dtFeat2.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtFeat2.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("SPFET2").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), chkbx.Text.Replace("'", "''"), _
                                                                       ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", "", "", chkbx.Text.Replace("'", "''"), "", "", "", "", "", "", "", "")
                    End If

                End If
            Next

            For j = 1 To ProduCount
                Dim cntl As Control = FindControl("chkBxProdu" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    Dim strProducer() As String = Regex.Split(chkbx.Text.Replace("'", "''"), ">")
                    Dim strText() As String = Regex.Split(strProducer(1), "<")
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "8", strText(0), "Y")

                    'Insert Special Features 1 data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvProdu = ds.Tables(0).DefaultView()
                        dvProdu.RowFilter = "PRODUCER IS NULL"
                        dtProdu = dvProdu.ToTable()

                        If dtProdu.Rows.Count > 0 Then
                            For i = 0 To dtProdu.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtProdu.Rows(i).Item("MODELID").ToString(), dtProdu.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                     strText(0), dtProdu.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtProdu.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("PRODUCER").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                        strText(0), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", "", "", "", strText(0), "", "", "", "", "", "", "")
                    End If

                End If
            Next

            For j = 1 To PackCount
                Dim cntl As Control = FindControl("chkBxPack" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    Dim strPacker() As String = Regex.Split(chkbx.Text.Replace("'", "''"), ">")
                    Dim strText() As String = Regex.Split(strPacker(1), "<")
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "9", strText(0), "Y")

                    'Insert Special Features 1 data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvPack = ds.Tables(0).DefaultView()
                        dvPack.RowFilter = "PACKER IS NULL"
                        dtPack = dvPack.ToTable()

                        If dtPack.Rows.Count > 0 Then
                            For i = 0 To dtPack.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtPack.Rows(i).Item("MODELID").ToString(), dtPack.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtPack.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtPack.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtPack.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtPack.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtPack.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtPack.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                     dtPack.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), strText(0), dtPack.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtPack.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtPack.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtPack.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtPack.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtPack.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("PACKER").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                        ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), strText(0), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", "", "", "", "", strText(0), "", "", "", "", "", "")
                    End If

                End If
            Next

            For j = 1 To GraphCount
                Dim cntl As Control = FindControl("chkBxGraph" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    Dim strGraphic() As String = Regex.Split(chkbx.Text.Replace("'", "''"), ">")
                    Dim strText() As String = Regex.Split(strGraphic(1), "<")
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "10", strText(0), "Y")

                    'Insert Special Features 1 data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvGraph = ds.Tables(0).DefaultView()
                        dvGraph.RowFilter = "GRAPHICS IS NULL"
                        dtGraph = dvGraph.ToTable()

                        If dtGraph.Rows.Count > 0 Then
                            For i = 0 To dtGraph.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtGraph.Rows(i).Item("MODELID").ToString(), dtGraph.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                     dtGraph.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("PACKER").ToString().Replace("'", "''"), strText(0), dtGraph.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtGraph.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("GRAPHICS").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                       ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), strText(0), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", "", "", "", "", "", strText(0), "", "", "", "", "")
                    End If
                End If
            Next

            For j = 1 To StructCount
                Dim cntl As Control = FindControl("chkBxStruct" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    Dim strStruct() As String = Regex.Split(chkbx.Text.Replace("'", "''"), ">")
                    Dim strText() As String = Regex.Split(strStruct(1), "<")
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "11", strText(0), "Y")

                    'Insert Special Features 1 data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvStruct = ds.Tables(0).DefaultView()
                        dvStruct.RowFilter = "STRUCTURES IS NULL"
                        dtStruct = dvStruct.ToTable()

                        If dtStruct.Rows.Count > 0 Then
                            For i = 0 To dtStruct.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtStruct.Rows(i).Item("MODELID").ToString(), dtStruct.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                     dtStruct.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), strText(0), dtStruct.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtStruct.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("STRUCTURES").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                        ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), strText(0), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", "", "", "", "", "", "", strText(0), "", "", "", "")
                    End If
                End If
            Next

            For j = 1 To FormCount
                Dim cntl As Control = FindControl("chkBxFormula" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    Dim strForm() As String = Regex.Split(chkbx.Text.Replace("'", "''"), ">")
                    Dim strText() As String = Regex.Split(strForm(1), "<")
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "12", strText(0), "Y")

                    'Insert Special Features 1 data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvForm = ds.Tables(0).DefaultView()
                        dvForm.RowFilter = "FORMULA IS NULL"
                        dtForm = dvForm.ToTable()

                        If dtForm.Rows.Count > 0 Then
                            For i = 0 To dtForm.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtForm.Rows(i).Item("MODELID").ToString(), dtForm.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtForm.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtForm.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtForm.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtForm.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtForm.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtForm.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                     dtForm.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtForm.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtForm.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtForm.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), strText(0), dtForm.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtForm.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtForm.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("FORMULA").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                        ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), strText(0), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", "", "", "", "", "", "", "", strText(0), "", "", "")
                    End If
                End If
            Next

            For j = 1 To ManufCount
                Dim cntl As Control = FindControl("chkBxManuf" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    Dim strManuf() As String = Regex.Split(chkbx.Text.Replace("'", "''"), ">")
                    Dim strText() As String = Regex.Split(strManuf(1), "<")
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "13", strText(0), "Y")

                    'Insert Special Features 1 data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvManuf = ds.Tables(0).DefaultView()
                        dvManuf.RowFilter = "MANUFACTORY IS NULL"
                        dtManuf = dvManuf.ToTable()

                        If dtManuf.Rows.Count > 0 Then
                            For i = 0 To dtManuf.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtManuf.Rows(i).Item("MODELID").ToString(), dtManuf.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                     dtManuf.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), strText(0), dtManuf.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), dtManuf.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                        ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), strText(0), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", "", "", "", "", "", "", "", "", strText(0), "", "")
                    End If
                End If
            Next

            For j = 1 To PackgCount
                Dim cntl As Control = FindControl("chkBxPackg" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    Dim strPackg() As String = Regex.Split(chkbx.Text.Replace("'", "''"), ">")
                    Dim strText() As String = Regex.Split(strPackg(1), "<")
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "14", strText(0), "Y")

                    'Insert Special Features 1 data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvPackg = ds.Tables(0).DefaultView()
                        dvPackg.RowFilter = "PACKAGING IS NULL"
                        dtPackg = dvPackg.ToTable()

                        If dtPackg.Rows.Count > 0 Then
                            For i = 0 To dtPackg.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtPackg.Rows(i).Item("MODELID").ToString(), dtPackg.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                     dtPackg.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtPackg.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), strText(0), dtPackg.Rows(i).Item("SKU").ToString().Replace("'", "''"), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("PACKAGING").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                        ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), strText(0), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", "", "", "", "", "", "", "", "", "", strText(0), "")
                    End If
                End If
            Next

            For j = 1 To SkuCount
                Dim cntl As Control = FindControl("chkBxSku" + j.ToString())
                chkbx = CType(cntl, CheckBox)
                If chkbx.Checked Then
                    Dim strSku() As String = Regex.Split(chkbx.Text.Replace("'", "''"), ">")
                    Dim strText() As String = Regex.Split(strSku(1), "<")
                    objUpIns.EditProjCategoryDetails(Session("ProjectId"), "15", strText(0), "Y")

                    'Insert Special Features 1 data into MODELDETAILS table
                    ds = objGetData.GetExistingModelDetails(Session("ProjectId"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        dvSku = ds.Tables(0).DefaultView()
                        dvSku.RowFilter = "SKU IS NULL"
                        dtSku = dvSku.ToTable()

                        If dtSku.Rows.Count > 0 Then
                            For i = 0 To dtSku.Rows.Count - 1
                                objUpIns.EditModelDetails(Session("ProjectId"), dtSku.Rows(i).Item("MODELID").ToString(), dtSku.Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), dtSku.Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), dtSku.Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), dtSku.Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), dtSku.Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), dtSku.Rows(i).Item("SPFET1").ToString().Replace("'", "''"), dtSku.Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                     dtSku.Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), dtSku.Rows(i).Item("PACKER").ToString().Replace("'", "''"), dtSku.Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), dtSku.Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), dtSku.Rows(i).Item("FORMULA").ToString().Replace("'", "''"), dtSku.Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), dtSku.Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), strText(0), "Y")
                            Next
                        Else
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                If ds.Tables(0).Rows(i).Item("SKU").ToString() <> chkbx.Text Then
                                    objUpIns.InsertModelDetails(Session("ProjectId"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"), _
                                                                        ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), strText(0))
                                End If
                            Next
                        End If
                    Else
                        objUpIns.InsertModelDetails(Session("ProjectId"), "", "", "", "", "", "", "", "", "", "", "", "", "", "", strText(0))
                    End If
                End If
            Next

            GetModelDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "RefreshPage", "RefreshPage();", True)
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Clicked on Generate Grid Button for Project:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

        Catch ex As Exception
            ErrorLable.Text = "Error:btnGrid_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try

            'PageDetails()


        Catch ex As Exception
            ErrorLable.Text = "Error:btnLoad_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCall.Click
        Try
            Dim objUpIns As New UpdateInsert()
            For Each i As GridViewRow In grdModel.Rows
                Dim Chk As New CheckBox
                Dim ModelId As New Integer
                Chk = i.FindControl("chkHide")
                ModelId = Convert.ToInt32(grdModel.DataKeys(i.RowIndex).Value)

                If Chk.Checked = True Then
                    objUpIns.ActivateModel(ModelId, Session("ProjectId"), "N")

                ElseIf Chk.Checked = False Then
                    objUpIns.ActivateModel(ModelId, Session("ProjectId"), "Y")

                End If
            Next


        Catch ex As Exception

        End Try
    End Sub


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdModel.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim tb As CheckBox = DirectCast(e.Row.Cells(1).FindControl("chkHide"), CheckBox)
            If tb.Checked = True Then
                tb.Enabled = True
            End If
            If Session("SavvyAnalyst") <> "Y" Then
                If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                    tb.Enabled = False
                End If
            End If

        End If

    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim msg As String
        Dim objUpIns As New UpdateInsert()
        Dim chk As CheckBox = DirectCast(sender, CheckBox)
        Dim gr As GridViewRow = DirectCast(chk.Parent.Parent, GridViewRow)

        msg = grdModel.DataKeys(gr.RowIndex).Value.ToString()
        If chk.Checked = True Then
            objUpIns.ActivateModel(msg, Session("ProjectId"), "N")
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Clicked on Grid Checkbox to Hide Model Id:" + msg + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Else
            objUpIns.ActivateModel(msg, Session("ProjectId"), "Y")
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Clicked on Grid Checkbox to Unhide Model Id:" + msg + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        End If

        GetModelDetails()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "RefreshPage", "RefreshPage();", True)
        'lblmsg.Text = "Hello";
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ds As New DataSet
        Dim dsSeq As New DataSet
        Dim SequenceId As New Integer
        Dim objGetData As New Selectdata()
        Dim objUpIns As New UpdateInsert()
        Try
            ds = Session("ModelData")
            dsSeq = objGetData.GetSequeceDetails(Session("ProjectId"))
            If dsSeq.Tables(0).Rows(0).Item("SEQUENCEID").ToString() = "" Then
                SequenceId = 1
            Else
                SequenceId = dsSeq.Tables(0).Rows(0).Item("SEQUENCEID") + 1
            End If

            objUpIns.InsertSaveProjectDetails(SequenceId, Session("ProjectId"), Session("UserId"))
            For i = 0 To ds.Tables(0).Rows.Count - 1
                objUpIns.InsertSaveModelDetails(SequenceId, Session("ProjectId"), ds.Tables(0).Rows(i).Item("MODELID").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PRODPACK").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKSIZE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKTYPE").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GEOGRAPHY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("VCHAIN").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET1").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SPFET2").ToString().Replace("'", "''"),
                                                 ds.Tables(0).Rows(i).Item("PRODUCER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKER").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("GRAPHICS").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("STRUCTURES").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("FORMULA").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("MANUFACTORY").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("PACKAGING").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("SKU").ToString().Replace("'", "''"), ds.Tables(0).Rows(i).Item("ISACTIVE").ToString())
            Next

            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "ModelInput.aspx", "Clicked on Save the Grid button for Project Id:" + Session("ProjectId") + "", Session("ProjectId"), Session("SPROJLogInCount").ToString())

            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "RefreshPage", "alert('Project Models saved succefully');", True)
        Catch ex As Exception

        End Try
    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function UpdateCase(ByVal Header As String, ByVal text As String) As String
        Try
            Dim ht As Hashtable = New Hashtable()
            Dim str() As String
            Dim seq As Integer = Convert.ToInt32(HttpContext.Current.Session("SeqCnt"))
            Dim flag As Boolean = False

            If HttpContext.Current.Session("ht") IsNot Nothing Then
                ht = DirectCast(HttpContext.Current.Session("ht"), Hashtable)
            End If

            For Each item As Object In ht
                str = item.Key.ToString().Split("-")
                If str(0) = Header Then
                    ht.Remove(item.Key)
                    flag = True
                    Exit For
                End If
            Next

            If flag Then
                ht.Add(Header + "-" + str(1).ToString(), text)
            Else
                ht.Add(Header + "-" + seq.ToString(), text)
                seq += 1
                HttpContext.Current.Session("SeqCnt") = seq
            End If
            HttpContext.Current.Session("ht") = ht
            Dim str1 As String = HttpContext.Current.Session("UserId").ToString()
            str1 = str1 + "Bhavesh"
            Return str1

        Catch ex As Exception

        End Try
    End Function

End Class
