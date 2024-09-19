Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain3_Assumptions_FixedCost
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

    Protected Function GetCaseIds() As String
        Dim CaseIds As String = String.Empty
        Dim objGetData As New S3GetData.Selectdata

        Try
            CaseIds = objGetData.Cases(AssumptionId)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try
        Return CaseIds
    End Function

    Protected Sub GetPageDetails()

        Dim ds As New DataSet
        Dim objGetData As New S3GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim DWidth As String = String.Empty
        Try
            CaseIds = GetCaseIds()
            ds = objGetData.PlantConfig2(CaseIds, UserName)
            DataCnt = ds.Tables(0).Rows.Count - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim ddl As DropDownList
            Dim txt As TextBox
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
                Cunits = Convert.ToInt32(ds.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(ds.Tables(0).Rows(i).Item("Units").ToString())

                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                If Cunits <> Units Then
                    Headertext = "Case#:" + ds.Tables(0).Rows(i).Item("CaseId").ToString() + "<br/>" + ds.Tables(0).Rows(i).Item("CaseDes").ToString() + "<br/> <span  style='color:red'>Unit Mismatch</span>"
                Else
                    Headertext = "Case#:" + ds.Tables(0).Rows(i).Item("CaseId").ToString() + "<br/>" + ds.Tables(0).Rows(i).Item("CaseDes").ToString()
                End If
                CaseDesp.Add(ds.Tables(0).Rows(i).Item("caseID").ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1, ds.Tables(0).Rows(i).Item("CaseId").ToString())
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)


            Dim trProduction As New TableRow
            Dim trWarehouse As New TableRow
            Dim trOffice As New TableRow
            Dim trSupport As New TableRow
            Dim trTotal As New TableRow





            tdInner = New TableCell
            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
            LeftTdSetting(tdInner, "Production" + Title + "", trProduction, "AlterNateColor2")
            trProduction.ID = "P_1"

            tdInner = New TableCell
            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
            LeftTdSetting(tdInner, "Warehouse" + Title + "", trWarehouse, "AlterNateColor1")
            trWarehouse.ID = "W_1"


            tdInner = New TableCell
            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
            LeftTdSetting(tdInner, "Office" + Title + "", trOffice, "AlterNateColor2")
            trOffice.ID = "O_1"

            tdInner = New TableCell
            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
            LeftTdSetting(tdInner, "Support" + Title + "", trSupport, "AlterNateColor1")
            trSupport.ID = "S_1"


            tdInner = New TableCell
            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
            LeftTdSetting(tdInner, "Total" + Title + "", trTotal, "AlterNateColor2")
            trTotal.ID = "T_1"







            For i = 0 To DataCnt


                'Production
                tdInner = New TableCell
                lbl = New Label
                tdInner.Text = lbl.Text + "<br/>" + FormatNumber(ds.Tables(0).Rows(i).Item("PRODUCTIONAREA").ToString(), 0)
                InnerTdSetting(tdInner, "", "Right")
                trProduction.Controls.Add(tdInner)


                'Warehouse
                tdInner = New TableCell
                lbl = New Label
                ' txt = New TextBox
                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("WAREHOUSEAREA").ToString(), 0)
                'TextBoxSetting(txt, "NormalTextBox")
                tdInner.Text = lbl.Text + "<br/>" + FormatNumber(ds.Tables(0).Rows(i).Item("WAREHOUSEAREA").ToString(), 0)
                InnerTdSetting(tdInner, "", "Right")
                'tdInner.Controls.Add(txt)
                trWarehouse.Controls.Add(tdInner)

                'Office
                tdInner = New TableCell
                lbl = New Label
                ' txt = New TextBox
                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("OFFICEAREA").ToString(), 0)
                'TextBoxSetting(txt, "NormalTextBox")
                tdInner.Text = lbl.Text + "<br/>" + FormatNumber(ds.Tables(0).Rows(i).Item("OFFICEAREA").ToString(), 0)
                InnerTdSetting(tdInner, "", "Right")
                'tdInner.Controls.Add(txt)
                'tdInner.Controls.Add(lbl)
                trOffice.Controls.Add(tdInner)


                'Support
                tdInner = New TableCell
                lbl = New Label
                ' txt = New TextBox
                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("SUPPORTAREA").ToString(), 0)
                'TextBoxSetting(txt, "NormalTextBox")
                tdInner.Text = lbl.Text + "<br/>" + FormatNumber(ds.Tables(0).Rows(i).Item("SUPPORTAREA").ToString(), 0)
                InnerTdSetting(tdInner, "", "Right")
                'tdInner.Controls.Add(txt)
                'tdInner.Controls.Add(lbl)
                trSupport.Controls.Add(tdInner)


                'Total
                tdInner = New TableCell
                lbl = New Label
                ' txt = New TextBox
                'txt.Text = FormatNumber(ds.Tables(0).Rows(i).Item("M2").ToString(), 0)
                'TextBoxSetting(txt, "NormalTextBox")

                tdInner.Text = lbl.Text + "<br/>" + FormatNumber(ds.Tables(0).Rows(i).Item("TOTALAREA").ToString(), 0)
                InnerTdSetting(tdInner, "", "Right")
                'tdInner.Controls.Add(txt)
                'tdInner.Controls.Add(lbl)
                trTotal.Controls.Add(tdInner)





            Next

            tblComparision.Controls.Add(trProduction)
            tblComparision.Controls.Add(trWarehouse)
            tblComparision.Controls.Add(trOffice)
            tblComparision.Controls.Add(trSupport)
            tblComparision.Controls.Add(trTotal)





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
            Td.Attributes.Add("onmouseover", "Tip('" + HeaderText + "')")
            Td.Attributes.Add("onmouseout", "UnTip()")



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
