Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain3_Assumptions_PalletInNew
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _strUserName
        End Get
        Set(ByVal Value As String)
            _strUserName = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _strPassword
        End Get
        Set(ByVal Value As String)
            _strPassword = Value
        End Set
    End Property

    Public Property AssumptionId() As Integer
        Get
            Return _iAssumptionId
        End Get
        Set(ByVal Value As Integer)
            _iAssumptionId = Value
        End Set
    End Property


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetSessionDetails()
                GetPageDetails()
            Catch ex As Exception

            End Try

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserName = Session("UserName")
            Password = Session("Password")
            AssumptionId = Session("AssumptionId")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0)
        Dim objGetData As New S3GetData.Selectdata

        Try
            CaseIds = objGetData.Cases1(AssumptionId)
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try
        Return CaseIds
    End Function

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
       
        Dim dstblPrefr As New DataSet
        Dim objGetData As New S1GetData.Selectdata
        Dim dsCaseDetails As New DataSet
        'Dim dstblPref As New DataSet
        'Dim dstblSug As New DataSet
        'Dim dstblMat As New DataSet
        Dim arrCaseID() As String
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim p As New Integer
        Dim DWidth As String = String.Empty
        Dim DsPal As New DataSet
        Dim DvPal As New DataView
        Dim DtPal As New DataTable
        Try
            DsPal = objGetData.GetPallets("-1", "", "")
            DvPal = DsPal.Tables(0).DefaultView

            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            For i = 0 To DataCnt
                ds = objGetData.GetPref(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstblPrefr.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;' />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"





            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstblPrefr.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstblPrefr.Tables(i).Rows(0).Item("Units").ToString())


                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                If Cunits <> Units Then
                    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<br/> <span  style='color:red'>Unit Mismatch</span>" + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                Else
                    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                End If
                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)
            Next
            tblComparision.Controls.Add(trHeader)


            For i = 1 To 10
                Dim z As New Integer
                Dim dsPref As New DataSet
                Dim dsSug As New DataSet
                Dim dsMat As New DataSet
                Dim dstblPref As New DataSet
                Dim dstblSug As New DataSet
                Dim dstblMat As New DataSet
                

                For z = 0 To DataCnt
                    'Preferred Value
                    dsPref = objGetData.GetRWPalletInPref(arrCaseID(z), Chr(64 + i), i)
                    dsPref.Tables(0).TableName = arrCaseID(z).ToString()
                    dstblPref.Tables.Add(dsPref.Tables(arrCaseID(z).ToString()).Copy())
                    'Suggested Value
                    dsSug = objGetData.GetRWPalletInSugg(arrCaseID(z), Chr(64 + i))
                    dsSug.Tables(0).TableName = arrCaseID(z).ToString()
                    dstblSug.Tables.Add(dsSug.Tables(arrCaseID(z).ToString()).Copy())
                    'Material Value
                    dsMat = objGetData.GetRWPalletMat(arrCaseID(z), i)
                    dsMat.Tables(0).TableName = arrCaseID(z).ToString()
                    dstblMat.Tables.Add(dsMat.Tables(arrCaseID(z).ToString()).Copy())
                Next

                'Showing Transport Unit Cube 
                If i = 1 Then
                    trInner = New TableRow()
                    tdInner = New TableCell
                    LeftTdSetting(tdInner, "<b>Transport Unit Cube ", trInner, "AlterNateColor1")
                    tdInner.Width = "300"
                    trInner.ID = "TUC_" + i.ToString()
                    For p = 0 To DataCnt
                        tdInner = New TableCell
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = FormatNumber(dstblPref.Tables(p).Rows(0).Item("TRANSUNITCUBE").ToString(), 3)
                        trInner.Controls.Add(tdInner)
                    Next
                    tblComparision.Controls.Add(trInner)
                End If

                'Showing Material
                trInner = New TableRow()
                tdInner = New TableCell
                LeftTdSetting(tdInner, "<b>Material" + i.ToString() + "</b>", trInner, "AlterNateColor4")
                tdInner.Width = "300"
                For p = 0 To DataCnt
                    tdInner = New TableCell
                    InnerTdSetting(tdInner, "", "Center")
                    If dstblMat.Tables(p).Rows(0).Item("UMDES").ToString() <> "" Then
                        tdInner.Text = dstblMat.Tables(p).Rows(0).Item("UMDES").ToString()
                    Else
                        tdInner.Text = dstblMat.Tables(p).Rows(0).Item("MATDES").ToString()
                    End If
                    trInner.Controls.Add(tdInner)
                Next
                tblComparision.Controls.Add(trInner)
                For j = 1 To 10
                    trInner = New TableRow()
                    For k = 1 To 19
                        trInner = New TableRow()
                        Select Case k
                            Case 1
                                tdInner = New TableCell
                                LeftTdSetting(tdInner, "<b>Layer" + j.ToString() + "</b>", trInner, "AlterNateColor5")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = ""
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 2
                                tdInner = New TableCell
                                trInner.ID = "A_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Item", trInner, "AlterNateColor1")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    'Dim DsItem As New DataSet
                                    'tdInner = New TableCell
                                    'InnerTdSetting(tdInner, "", "Center")
                                    'DsItem = objGetData.GetPallets(CInt(dstblPref.Tables(p).Rows(0).Item("PATID" + j.ToString())), "", "")
                                    'tdInner.Text = DsItem.Tables(0).Rows(0).Item("PalletDes").ToString()
                                    'InnerTdSetting(tdInner, DWidth, "Right")
                                    'trInner.Controls.Add(tdInner)

                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    DvPal.RowFilter = "PALLETID=" + dstblPref.Tables(p).Rows(0).Item("PATID" + j.ToString()).ToString() + ""
                                    DtPal = DvPal.ToTable()
                                    tdInner.Text = DtPal.Rows(0).Item("PalletDes").ToString()
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)

                                Next
                            Case 3
                                tdInner = New TableCell
                                Title = "(each)"
                                trInner.ID = "B_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Number " + Title, trInner, "AlterNateColor2")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblPref.Tables(p).Rows(0).Item("NUM" + j.ToString()), 0)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 4
                                tdInner = New TableCell
                                Title = "(" + dstblPref.Tables(0).Rows(0).Item("TITLE8") + ")"
                                trInner.ID = "C_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Weight " + Title, trInner, "AlterNateColor1")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblSug.Tables(p).Rows(0).Item("WEIGHT" + j.ToString()), 2)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 5
                                tdInner = New TableCell
                                Title = "(MJ/" + dstblPref.Tables(0).Rows(0).Item("TITLE8") + " Mat.)"
                                trInner.ID = "D_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Energy Suggested " + Title, trInner, "AlterNateColor2")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblSug.Tables(p).Rows(0).Item("ERGY" + j.ToString()), 2)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 6
                                tdInner = New TableCell
                                Title = "(MJ/" + dstblPref.Tables(0).Rows(0).Item("TITLE8") + " Mat.)"
                                trInner.ID = "E_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Energy Preferred " + Title, trInner, "AlterNateColor1")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblPref.Tables(p).Rows(0).Item("ERGY" + j.ToString()), 3)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 7
                                tdInner = New TableCell
                                Title = "(" + dstblPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/" + dstblPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " Mat.)"
                                trInner.ID = "F_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "CO2 Equivalent Suggested " + Title, trInner, "AlterNateColor2")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblSug.Tables(p).Rows(0).Item("GHG" + j.ToString()), 2)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 8
                                tdInner = New TableCell
                                Title = "(" + dstblPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/" + dstblPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " Mat.)"
                                trInner.ID = "G_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "CO2 Equivalent Preferred " + Title, trInner, "AlterNateColor1")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblPref.Tables(p).Rows(0).Item("GHG" + j.ToString()), 3)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 9
                                tdInner = New TableCell
                                Title = "(" + dstblPref.Tables(0).Rows(0).Item("TITLE10").ToString() + " Water/" + dstblPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " Mat.)"
                                trInner.ID = "WS_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Water Sugg. " + Title, trInner, "AlterNateColor2")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblSug.Tables(p).Rows(0).Item("WATER" + j.ToString()), 2)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 10
                                tdInner = New TableCell
                                Title = "(" + dstblPref.Tables(0).Rows(0).Item("TITLE10").ToString() + " Water/" + dstblPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " Mat.)"
                                trInner.ID = "WP_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Water Pref. " + Title, trInner, "AlterNateColor1")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblPref.Tables(p).Rows(0).Item("WATER" + j.ToString()), 3)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 11
                                tdInner = New TableCell
                                Title = "(%)"
                                trInner.ID = "H_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Recycle " + Title, trInner, "AlterNateColor2")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblPref.Tables(p).Rows(0).Item("RECYCLE" + j.ToString()), 3)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 12
                                tdInner = New TableCell
                                Title = "(Unitless)"
                                trInner.ID = "I_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Recovery Suggested " + Title, trInner, "AlterNateColor1")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblSug.Tables(p).Rows(0).Item("RECO" + j.ToString()), 0)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 13
                                tdInner = New TableCell
                                Title = "(Unitless)"
                                trInner.ID = "J_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Recovery Preferred " + Title, trInner, "AlterNateColor2")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblPref.Tables(p).Rows(0).Item("RECO" + j.ToString()), 3)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 14
                                tdInner = New TableCell
                                Title = "(Unitless)"
                                trInner.ID = "K_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Sustainable Materials Suggested " + Title, trInner, "AlterNateColor1")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = dstblSug.Tables(p).Rows(0).Item("OSHA" + j.ToString())
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 15
                                tdInner = New TableCell
                                trInner.ID = "L_" + j.ToString() + "_" + i.ToString()
                                Title = "(Unitless)"
                                LeftTdSetting(tdInner, "Sustainable Materials Preferred " + Title, trInner, "AlterNateColor2")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    If dstblPref.Tables(p).Rows(0).Item("OSHA" + j.ToString()).ToString().Trim().Length() > 0 Then
                                        tdInner.Text = dstblPref.Tables(p).Rows(0).Item("OSHA" + j.ToString())
                                    Else
                                        tdInner.Text = "Nothing"
                                    End If
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 16
                                tdInner = New TableCell
                                Title = "(Unitless)"
                                trInner.ID = "M_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "PC Recycle Suggested " + Title, trInner, "AlterNateColor1")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblSug.Tables(p).Rows(0).Item("PC" + j.ToString()), 2)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 17
                                tdInner = New TableCell
                                Title = "(Unitless)"
                                trInner.ID = "N_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "PC Recycle Preferred " + Title, trInner, "AlterNateColor2")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblPref.Tables(p).Rows(0).Item("PC" + j.ToString()), 3)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 18
                                tdInner = New TableCell
                                Title = "(" + dstblPref.Tables(0).Rows(0).Item("TITLE4").ToString() + ")"
                                trInner.ID = "O_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Ship Suggested " + Title, trInner, "AlterNateColor1")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblSug.Tables(p).Rows(0).Item("SHIP" + j.ToString()), 0)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                            Case 19
                                tdInner = New TableCell
                                Title = "(" + dstblPref.Tables(0).Rows(0).Item("TITLE4").ToString() + ")"
                                trInner.ID = "P_" + j.ToString() + "_" + i.ToString()
                                LeftTdSetting(tdInner, "Ship Preferred " + Title, trInner, "AlterNateColor2")
                                tdInner.Width = "300"
                                For p = 0 To DataCnt
                                    Dim DsItem As New DataSet
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "", "Center")
                                    tdInner.Text = FormatNumber(dstblPref.Tables(p).Rows(0).Item("SHIP" + j.ToString()), 3)
                                    InnerTdSetting(tdInner, DWidth, "Right")
                                    trInner.Controls.Add(tdInner)
                                Next
                        End Select
                        tblComparision.Controls.Add(trInner)
                    Next
                Next
            Next








        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
  
    Protected Sub GetSustainMaterialDetails(ByVal MatDes As String)
        Dim Ds As New DataSet
        Dim lbl As New Label
        Dim ObjGetdata As New S1GetData.Selectdata()

        Dim Path As String = String.Empty

        Try
            If MatDes.Trim().Length > 0 Then
                lbl.Text = MatDes.ToString()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

   

    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 30
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try

            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)
            If Align = "Left" Then
                Td.Style.Add("padding-left", "5px")
            End If
            If Align = "Right" Then
                Td.Style.Add("padding-right", "5px")
            End If




        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try


            txt.CssClass = Css
            txt.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css




        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    'Protected Function GetPalletCombo(ByVal InventoryType As String, ByVal Effdate As String) As DropDownList
    '    Dim ddl As New DropDownList
    '    Dim ds As New DataSet()
    '    Dim objGetData As New SGetData.Selectdata
    '    Try
    '        ds = objGetData.GetPalletItems(InventoryType, Effdate)
    '        With ddl
    '            .DataSource = ds
    '            .DataTextField = "PalletDes"
    '            .DataValueField = "PalletId"
    '            .DataBind()
    '            .CssClass = "DropDown"
    '            .Width = 300
    '            .Enabled = False
    '        End With


    '    Catch ex As Exception
    '        _lErrorLble.Text = "Error:GetMateralCombo:" + ex.Message.ToString()
    '    End Try
    '    Return ddl
    'End Function




End Class
