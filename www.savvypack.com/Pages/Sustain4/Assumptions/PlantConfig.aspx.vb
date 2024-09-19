Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S4GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain4_Assumptions_PlantConfig
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
        Dim CaseIds(0) As String
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
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String

        Try
            arrCaseID = GetCaseIds()
            'ds = objGetData.PlantConfig(CaseIds, UserName)
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"


            For i = 0 To DataCnt

                ds = objGetData.GetPlantConfigDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next

            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())


                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                'Headertext = "Case#:" + ds.Tables(0).Rows(i).Item("CaseId").ToString() + "<br/>" + ds.Tables(0).Rows(i).Item("CaseDes").ToString()
                Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"

                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)

            For i = 1 To 10
                For j = 1 To 11
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Department " + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                'Break
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Required Departments", trInner, "AlterNateColor1")
                            trInner.ID = "RD_" + i.ToString()
                            For k = 0 To DataCnt
                                'Required Departments
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl = GetDepartment()
                                ddl.SelectedValue = dstbl.Tables(k).Rows(0).Item("DA" + i.ToString()).ToString()
                                tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 3
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Next Process 1", trInner, "AlterNateColor2")
                            trInner.ID = "NP1_" + i.ToString()
                            For k = 0 To DataCnt
                                'Next Process 1
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl = GetDepartment()
                                ddl.SelectedValue = dstbl.Tables(k).Rows(0).Item("DB" + i.ToString()).ToString()
                                tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Next Process 2", trInner, "AlterNateColor1")
                            trInner.ID = "NP2_" + i.ToString()
                            For k = 0 To DataCnt
                                'Next Process 2
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl = GetDepartment()
                                ddl.SelectedValue = dstbl.Tables(k).Rows(0).Item("DC" + i.ToString()).ToString()
                                tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 5
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Next Process 3", trInner, "AlterNateColor2")
                            trInner.ID = "NP3_" + i.ToString()
                            For k = 0 To DataCnt    'Next Process 3
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl = GetDepartment()
                                ddl.SelectedValue = dstbl.Tables(k).Rows(0).Item("DD" + i.ToString()).ToString()
                                tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 6
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Next Process 4", trInner, "AlterNateColor1")
                            trInner.ID = "NP4_" + i.ToString()
                            For k = 0 To DataCnt
                                'Next Process 4
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl = GetDepartment()
                                ddl.SelectedValue = dstbl.Tables(k).Rows(0).Item("DE" + i.ToString()).ToString()
                                tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 7
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Next Process 5", trInner, "AlterNateColor2")
                            trInner.ID = "NP5_" + i.ToString()
                            For k = 0 To DataCnt
                                'Next Process 5
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl = GetDepartment()
                                ddl.SelectedValue = dstbl.Tables(k).Rows(0).Item("DF" + i.ToString()).ToString()
                                tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 8

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Next Process 6", trInner, "AlterNateColor1")
                            trInner.ID = "NP6_" + i.ToString()
                            For k = 0 To DataCnt
                                'Next Process 6
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl = GetDepartment()
                                ddl.SelectedValue = dstbl.Tables(k).Rows(0).Item("DG" + i.ToString()).ToString()
                                tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 9

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Next Process 7", trInner, "AlterNateColor2")
                            trInner.ID = "NP7_" + i.ToString()
                            For k = 0 To DataCnt
                                'Next Process 7
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl = GetDepartment()
                                ddl.SelectedValue = dstbl.Tables(k).Rows(0).Item("DH" + i.ToString()).ToString()
                                tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 10

                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Next Process 8", trInner, "AlterNateColor1")
                            trInner.ID = "NP8_" + i.ToString()
                            For k = 0 To DataCnt
                                'Next Process 8
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl = GetDepartment()
                                ddl.SelectedValue = dstbl.Tables(k).Rows(0).Item("DI" + i.ToString()).ToString()
                                tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 11
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Next Process 9", trInner, "AlterNateColor2")
                            trInner.ID = "NP9_" + i.ToString()
                            For k = 0 To DataCnt
                                'Next Process 9
                                tdInner = New TableCell
                                ddl = New DropDownList
                                ddl = GetDepartment()
                                ddl.SelectedValue = dstbl.Tables(k).Rows(0).Item("DJ" + i.ToString()).ToString()
                                tdInner.Text = ddl.SelectedItem.Text
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'tdInner.Controls.Add(ddl)
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

    Protected Function GetDepartment() As DropDownList
        Dim ddl As New DropDownList
        Dim ds As New DataSet()
        Dim objGetData As New S4GetData.Selectdata
        Try
            ds = objGetData.GetDepartment
            With ddl
                .DataSource = ds
                .DataTextField = "PROCDE"
                .DataValueField = "PROCID"
                .DataBind()
                .CssClass = "DropDown"
                .Width = 150
                .Enabled = False

            End With


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMateralCombo:" + ex.Message.ToString()
        End Try
        Return ddl
    End Function
End Class
