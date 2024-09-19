Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Universal_loginN_Pages_ErrorU
    Inherits System.Web.UI.Page
    Dim _strErrorCode As String
    Dim _strSchema As String
    Dim _strSerID As String

    Public Property ErrorCode() As String
        Get
            Return _strErrorCode
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strErrorCode = obj.Decrypt(value)
        End Set
    End Property
    Public Property Schema() As String
        Get
            Return _strSchema
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strSchema = obj.Decrypt(value)
        End Set
    End Property

    Public Property ServID() As String
        Get
            Return _strSerID
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strSerID = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ErrorCode = Request.QueryString("ErrorCode").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            ServID = Request.QueryString("ServID").ToString()
            If Request.QueryString("Schema") <> Nothing Then
                Schema = Request.QueryString("Schema").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            End If

            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet()
        Dim obj As New CryptoHelper
        Try
            ds = objGetData.GetErrors(ErrorCode)
            lblErrorCode.Text = ds.Tables(0).Rows(0).Item("ERRORCODE").ToString()
            lblErrorMessage.Text = ds.Tables(0).Rows(0).Item("ERRORDE1").ToString()
            If ds.Tables(0).Rows(0).Item("ERRORTYPE").ToString() = "LOGIN" Then
                divUpdate.Visible = True
                If ErrorCode = "ALDE115" Then
                    lblText.Text = " to go to the LogOn Renewal Page."
                    hypPage.NavigateUrl = "ULogRenewal.aspx?Schema=" + obj.Encrypt(Schema).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + ServID
                    'hypPage.NavigateUrl = "ULogRenewal.aspx"
                    'GetTableDetails()
                    If Schema = "ReportConnectionString" Or Schema = "ContractConnectionString" Or Schema = "PackProdConnectionString" Then
                        GetTableDetails2()
                    Else
                        GetTableDetails1()
                    End If
                ElseIf ErrorCode = "ALDE114" Then
                    lblText.Text = " to go SavvyPack Corporation's Home Page."
                    hypPage.NavigateUrl = "~/Index.aspx"
                ElseIf ErrorCode = "ALDE113" Then
                    lblText.Text = " to go Previous Page."
                    hypPage.NavigateUrl = "../" + DirectCast(Request.UrlReferrer, System.Uri).Segments(Request.UrlReferrer.Segments.Length - 2) + DirectCast(Request.UrlReferrer, System.Uri).Segments(Request.UrlReferrer.Segments.Length - 1) + ""
                ElseIf ErrorCode = "ALDE116" Then
                    lblText.Text = " to go User Management Page."
                    hypPage.NavigateUrl = "CorpUser.aspx"
                End If


            Else
                divUpdate.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetTableDetails1()
        Dim objLoginData As New LoginGetData.Selectdata()
        Dim strUserName As String = String.Empty
        Dim dsData As New DataSet()
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim dsSev As New DataSet
        Dim dsNew As New DataSet




        Try
            dsSev = objLoginData.GetModuleDetailsByLicense(Session("LicenseNo").ToString())
            If dsSev.Tables(0).Rows.Count > 0 Then
                Dim dsD(dsSev.Tables(0).Rows.Count) As DataSet
                Dim modN(dsSev.Tables(0).Rows.Count) As String
                Dim flag(dsSev.Tables(0).Rows.Count) As String
                For i = 0 To dsSev.Tables(0).Rows.Count - 1
                    dsD(i) = New DataSet
                    flag(i) = "Y"
                    If dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "ECON1" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "EconConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "ECON2" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "Econ2ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "ECON3" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "Econ3ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "ECON4" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "Econ4ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "SUSTAIN1" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "Sustain1ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "SUSTAIN2" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "Sustain2ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "SUSTAIN3" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "Sustain3ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "SUSTAIN4" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "Sustain4ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "SCHEM1" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "Schem1ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "ECHEM1" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "Echem1ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "MARKET1" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicenseMKT(Session("LicenseNo").ToString(), "Market1ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "SPEC" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "SpecConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "EDISTRIBUTION" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "DistributionConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "SDISTRIBUTION" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "SDistributionConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "RETAIL" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "RetailConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "VALUE CHAIN MODULE" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "VChainConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "COMPECON" Then
                        dsD(i) = objLoginData.GetCOMPInuseDetailsByLicense(Session("LicenseNo").ToString(), "EconConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "COMPSUSTAIN" Then
                        dsD(i) = objLoginData.GetCOMPInuseDetailsByLicense(Session("LicenseNo").ToString(), "Sustain1ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "STANDASSIST" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "SBAConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "MOLDE1" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "MoldE1ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "MOLDE2" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "MoldE2ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "MOLDS1" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "MoldS1ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "MOLDS2" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "MoldS2ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "MEDE1" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "MedEcon1ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "MEDE2" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "MedEcon2ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "MEDS1" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "MedSustain1ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()

                    ElseIf dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper() = "MEDS2" Then
                        dsD(i) = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), "MedSustain2ConnectionString")
                        modN(i) = dsSev.Tables(0).Rows(i).Item("SERVICEDE").ToString().ToUpper()
                    Else
                        dsD(i) = Nothing
                        flag(i) = "N"
                    End If
                Next
                dsNew = bindDataSet(dsD, modN, flag)

            End If

            'User Details
            Dim p As Integer
            trHeader = New TableRow
            trHeader2 = New TableRow
            For p = 1 To 3
                tdHeader = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header 

                Select Case p
                    Case 1
                        HeaderTdSetting(tdHeader, "270px", "User(s) That is logged In:", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", "Log In Time", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 3
                        HeaderTdSetting(tdHeader, "180px", "Module", "1")
                        trHeader.Controls.Add(tdHeader)
                End Select
            Next
            tblComparision.Controls.Add(trHeader)

            'Inner()
            Dim q As Integer
            trInner = New TableRow
            For i = 0 To dsNew.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For q = 1 To 3
                    tdInner = New TableCell

                    Select Case q
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = dsNew.Tables(0).Rows(i).Item("USERNAME").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = dsNew.Tables(0).Rows(i).Item("SERVERDATE").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = dsNew.Tables(0).Rows(i).Item("MODULE").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblComparision.Controls.Add(trInner)

            Next




        Catch ex As Exception
            lblError.Text = "Error:GetTableDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetTableDetails2()
        Dim objLoginData As New LoginGetData.Selectdata()
        Dim strUserName As String = String.Empty
        Dim dsData As New DataSet()
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Try
            dsData = objLoginData.GetInuseDetailsByLicense(Session("LicenseNo").ToString(), Schema)

            'User Details
            Dim p As Integer
            trHeader = New TableRow
            trHeader2 = New TableRow
            For p = 1 To 2
                tdHeader = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header 

                Select Case p
                    Case 1
                        HeaderTdSetting(tdHeader, "270px", "User(s) That Is Logged In:", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", "Log In Time", "1")
                        trHeader.Controls.Add(tdHeader)
                End Select
            Next
            tblComparision.Controls.Add(trHeader)

            'Inner()
            Dim q As Integer
            trInner = New TableRow
            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For q = 1 To 3
                    tdInner = New TableCell

                    Select Case q
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = dsData.Tables(0).Rows(i).Item("USERNAME").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = dsData.Tables(0).Rows(i).Item("SERVERDATE").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblComparision.Controls.Add(trInner)

            Next




        Catch ex As Exception
            lblError.Text = "Error:GetTableDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Public Function bindDataSet(ByVal ds() As DataSet, ByVal modName() As String, ByVal flag() As String) As DataSet
        Dim dsNew As New DataSet()
        Try

            Dim tbl As New DataTable()
            Dim row As DataRow


            Dim userName As DataColumn = New DataColumn("USERNAME")
            userName.DataType = System.Type.GetType("System.String")
            tbl.Columns.Add(userName)

            Dim serverDate As DataColumn = New DataColumn("SERVERDATE")
            serverDate.DataType = System.Type.GetType("System.String")
            tbl.Columns.Add(serverDate)

            Dim moduleName As DataColumn = New DataColumn("MODULE")
            moduleName.DataType = System.Type.GetType("System.String")
            tbl.Columns.Add(moduleName)

            Dim i As Integer
            Dim k As Integer

            For k = 0 To ds.Length - 1
                If flag(k) = "Y" Then
                    For i = 0 To ds(k).Tables(0).Rows.Count - 1
                        If ds(k).Tables(0).Rows.Count > 0 Then
                            row = tbl.NewRow()
                            row.Item("USERNAME") = ds(k).Tables(0).Rows(i).Item("USERNAME").ToString()
                            row.Item("MODULE") = modName(k)
                            row.Item("SERVERDATE") = ds(k).Tables(0).Rows(i).Item("SERVERDATE").ToString()
                            tbl.Rows.Add(row)
                        End If

                    Next
                End If

            Next
            dsNew.Tables.Add(tbl)
            Return dsNew
        Catch ex As Exception
            Return dsNew
        End Try

    End Function
    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub Header2TdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Font.Size = 8
            Td.Height = 20
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

        Catch ex As Exception
            lblError.Text = "Error:TextBoxSetting:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception
            lblError.Text = "Error:LableSetting:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css
        Catch ex As Exception
            lblError.Text = "Error:LeftTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
End Class
