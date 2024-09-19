Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain4_Assumptions_Preferences
    Inherits System.Web.UI.Page
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
        Dim CaseIds() As String
        Dim objGetData As New S4GetData.Selectdata

        Try
            CaseIds = objGetData.Cases1(AssumptionId)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try
        Return CaseIds

    End Function


    Protected Sub GetPageDetails()

        Dim ds As New DataSet
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New S2GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.GetPref(CaseIds)
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim ddl As DropDownList

            Dim txt As TextBox
            Dim k As New Integer
            Dim j As New Integer
            Dim lbl As Label
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty

            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:160px;height:0px;'  />", 1, "")
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                'Dim ds As New DataSet
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
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1, arrCaseID(i))
                trHeader.Controls.Add(tdHeader)

            Next
            tblComparision.Controls.Add(trHeader)







            For i = 1 To 1
                For j = 1 To 5
                    Dim trInner As New TableRow
                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            Dim var As New Integer
                            LeftTdSetting(tdInner, "Units", trInner, "AlterNateColor1")
                            trInner.ID = "UNIT_1"
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                var = dstbl.Tables(k).Rows(0).Item("UNITS").ToString()
                                If var = 0 Then
                                    tdInner.Text = "English"
                                Else
                                    tdInner.Text = "Metric"
                                End If
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)

                            Next

                         


                        Case 2
                         
                            tdInner = New TableCell
                            Dim dsMat As New DataSet
                            LeftTdSetting(tdInner, "LCI Data", trInner, "AlterNateColor2")
                            trInner.ID = "INV_1"
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                dsMat = objGetData.GetLCI(dstbl.Tables(k).Rows(0).Item("INVENTORYTYPE").ToString())
                                tdInner.Text = dsMat.Tables(0).Rows(0).Item("INVENTORY").ToString()
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            
                            tdInner = New TableCell
                            Dim dsMat As New DataSet
                            LeftTdSetting(tdInner, "Effective Date", trInner, "AlterNateColor1")
                            trInner.ID = "EDATE_1"
                            For k = 0 To DataCnt
                                tdInner = New TableCell

                                tdInner.Text = dstbl.Tables(k).Rows(0).Item("EDATE").ToString()
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 4
                            tdInner = New TableCell
                            Dim var As String
                            LeftTdSetting(tdInner, " Preferred Volume", trInner, "AlterNateColor2")
                            trInner.ID = "PREF_1"
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                var = dstbl.Tables(k).Rows(0).Item("PVOLUSE").ToString()
                                If var = "0" Then
                                    tdInner.Text = "Product Volume With Package Volume"
                                Else
                                    tdInner.Text = "Product Volume"
                                End If
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)

                            Next
                        Case 5
                            tdInner = New TableCell
                            Dim var As String
                            LeftTdSetting(tdInner, "Energy Calculations", trInner, "AlterNateColor1")
                            trInner.ID = "CAL_1"
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                var = dstbl.Tables(k).Rows(0).Item("ERGYCALC").ToString()
                                If var = "Y" Then
                                    tdInner.Text = "Adjust Automatically"
                                Else
                                    tdInner.Text = " Use Capacity"
                                End If
                                InnerTdSetting(tdInner, "", "Right")
                                trInner.Controls.Add(tdInner)

                            Next


                    End Select

                    tblComparision.Controls.Add(trInner)
                Next
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String, ByVal CaseId As String)
        Try
            If CaseId.Trim() <> "" Then
                Td.Text = HeaderText
            End If

            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 30
            Td.HorizontalAlign = HorizontalAlign.Center
        Catch ex As Exception
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try


            txt.CssClass = Css
            txt.Enabled = False
        Catch ex As Exception
            _lErrorLble.Text = "Error:TextBoxSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css




        Catch ex As Exception
            _lErrorLble.Text = "Error:LeftTdSetting:" + ex.Message.ToString()
        End Try
    End Sub



End Class
