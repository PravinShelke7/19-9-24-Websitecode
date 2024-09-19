Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ4_Assumptions_Preferences
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer
    Dim _btnUpdate As ImageButton


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

    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
        End Set
    End Property


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = False

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetUpdatebtn()
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
            AssumptionId = Session("AssumptionID")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0) As String
        Dim objGetData As New E4GetData.Selectdata
        Try
            CaseIds = objGetData.Cases(AssumptionId)
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try

    End Function

    Protected Sub GetPageDetails()
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New E2GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String


        Try
            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim lbl As New Label
            Dim lbl2 As New Label
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                Dim ds As New DataSet
                ds = objGetData.GetPref(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next





            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())


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
            For i = 1 To 1
                For j = 1 To 7
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Country of Manufacture: ", trInner, "")
                            trInner.ID = "CM" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label()
                                tdInner = New TableCell
                                GetCountryDetails(lbl, dstbl.Tables(k).Rows(0).Item("OCOUNTRY").ToString())
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Country of Destination", trInner, "")
                            trInner.ID = "CD" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label()
                                tdInner = New TableCell
                                GetCountryDetails(lbl, dstbl.Tables(k).Rows(0).Item("DCOUNTRY").ToString())
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Prefferred Unit", trInner, "")
                            trInner.ID = "PREFU" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label()
                                tdInner = New TableCell
                                GetUnitDetails(lbl, dstbl.Tables(k).Rows(0).Item("UNITS").ToString())
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Effective Date", trInner, "")
                            trInner.ID = "EFFDATE" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label()
                                lbl.Text = dstbl.Tables(k).Rows(0).Item("EDATE").ToString()
                                tdInner = New TableCell
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Preferred Currency", trInner, "")
                            trInner.ID = "PREFCUR" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label()
                                GetCurrencyDetails(lbl, dstbl.Tables(k).Rows(0).Item("CURRENCY").ToString())
                                tdInner = New TableCell
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 6
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Preferred Volume", trInner, "")
                            trInner.ID = "PVOL" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label()
                                If dstbl.Tables(k).Rows(0).Item("PVOLUSE").ToString() = "0" Then
                                    lbl.Text = "Product Volume With Package Volume"
                                Else
                                    lbl.Text = "Product Volume"
                                End If
                                tdInner = New TableCell
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 7
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Energy Calculations", trInner, "")
                            trInner.ID = "ERGYCAL" + i.ToString()
                            For k = 0 To DataCnt
                                lbl = New Label()
                                If dstbl.Tables(k).Rows(0).Item("ERGYCALC").ToString() = "Y" Then
                                    lbl.Text = "Adjust Automatically"
                                Else
                                    lbl.Text = "Use Capacity "
                                End If
                                tdInner = New TableCell
                                tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                    End Select

                    If (j Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If



                    tblComparision.Controls.Add(trInner)
                Next
            Next



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
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

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String, ByVal CaseId As Integer)
        Try
            txt.CssClass = Css
            If CaseId <= 1000 And Session("Password") <> "9krh65sve3" Then
                txt.Enabled = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

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

    Protected Sub GetCountryDetails(ByRef lbl As Label, ByVal CountryId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetCountry(CountryId)
            lbl.Text = Ds.Tables(0).Rows(0).Item("COUNTRYDE1").ToString()

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetUnitDetails(ByRef lbl As Label, ByVal UnitId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            If UnitId = 0 Then
                lbl.Text = "English"
            Else
                lbl.Text = "Metric"
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetCurrencyDetails(ByRef lbl As Label, ByVal currId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetCurrancy(currId)
            lbl.Text = Ds.Tables(0).Rows(0).Item("CURDE1").ToString()

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
