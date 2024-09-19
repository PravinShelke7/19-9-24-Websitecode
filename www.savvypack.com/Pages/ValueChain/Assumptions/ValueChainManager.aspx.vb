Imports System.Data
Imports System.Data.OleDb
Imports System
Imports VChainGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_ValueChain_Assumptions_ValueChainManager
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iValueChainId As Integer


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

    Public Property ValueChainId() As Integer
        Get
            Return _iValueChainId
        End Get
        Set(ByVal Value As Integer)
            _iValueChainId = Value
        End Set
    End Property

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            GetErrorLable()
            GetSessionDetails()
            GetPageDetails()

        Catch ex As Exception

        End Try
       
    End Sub
    Protected Sub GetSessionDetails()
        Dim dt As New DataTable
        Dim objGetData As New Selectdata
        Try
            UserName = Session("UserName")
            Password = Session("Password")
            ValueChainId = Session("ValueChainId")
            dt = objGetData.GetDescription(ValueChainId)
            Session("VALUECHAINNAME") = dt.Rows(0).Item("VALUECHAINNAME")
            lblAID.Text = ValueChainId
            lblAdes.Text = Session("VALUECHAINNAME")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetPageDetails()
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim objGetData As New Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdInner As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Try
            dt = objGetData.GetDescription(ValueChainId)
            ds = objGetData.GetAllVChainCases(Session("ValueChainId").ToString(), dt.Rows(0).Item("MODNAME").ToString(), dt.Rows(0).Item("RESULTCASES").ToString())

            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "40px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "90px", "Module Name", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "70px", "CaseId", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "350px", "Case Description", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "200px", "Results", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblCaseMngr.Controls.Add(trHeader)



            'Inner
            For i = 0 To ds.Tables(0).Rows.Count
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Item
                            InnerTdSetting(tdInner, "", "Left")
                            tdInner.Text = "<b>" + (i + 1).ToString() + "</b>"
                            trInner.Controls.Add(tdInner)

                        Case 2
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.CssClass = "Label"
                            If i = 0 Then
                                lbl.Text = dt.Rows(0).Item("MODNAME").ToString()
                            Else
                                lbl.Text = ds.Tables(0).Rows(i - 1).Item("MODNAME").ToString()
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "Label"
                            If i = 0 Then
                                lbl.Text = dt.Rows(0).Item("RESULTCASES").ToString()
                            Else
                                lbl.Text = ds.Tables(0).Rows(i - 1).Item("MODCASEID").ToString()
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Left")
                            If i <> ds.Tables(0).Rows.Count Then
                                lbl = New Label
                                lbl.CssClass = "Label"
                                'lbl.Text = ""
                                If i = 0 Then
                                    GetCaseDescription(lbl, dt.Rows(0).Item("MODNAME").ToString(), dt.Rows(0).Item("RESULTCASES").ToString())
                                Else
                                    GetCaseDescription(lbl, ds.Tables(0).Rows(i - 1).Item("MODNAME").ToString(), ds.Tables(0).Rows(i - 1).Item("MODCASEID").ToString())
                                End If
                                tdInner.Controls.Add(lbl)
                            Else
                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypCaseDes" + i.ToString()
                                hid.ID = "hidCaseId" + i.ToString()
                                'Link.Width = 120
                                Link.CssClass = "Link"
                                GetCaseDescription(Link, hid, ds.Tables(0).Rows(i - 1).Item("MODNAME").ToString(), ds.Tables(0).Rows(i - 1).Item("MODCASEID").ToString())
                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                            End If

                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypProfitLoss" + i.ToString()
                            hid.ID = "hidCaseId" + i.ToString() + "_" + j.ToString()
                            'Link.Width = 120
                            Link.Text = "Profit And Loss"
                            Link.CssClass = "Link"
                            If i = 0 Then
                                Link.NavigateUrl = "../Results/Resultspl.aspx?ModName=" + dt.Rows(0).Item("MODNAME").ToString() + "&CaseId=" + dt.Rows(0).Item("RESULTCASES").ToString()
                            Else
                              
                                Link.NavigateUrl = "../Results/Resultspl.aspx?ModName=" + ds.Tables(0).Rows(i - 1).Item("MODNAME").ToString() + "&CaseId=" + ds.Tables(0).Rows(i - 1).Item("MODCASEID").ToString()                                
                            End If
                            Link.Target = "_blank"
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                    End Select
                    If i Mod 2 = 0 Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                Next


                tblCaseMngr.Controls.Add(trInner)
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
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center
        Catch ex As Exception

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
            Td.Height = 20
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GetCaseDescription(ByVal lbl As Label, ByVal ModName As String, ByVal CaseId As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New VChainGetData.Selectdata()
        Try
            Ds = ObjGetdata.GetCases(CaseId, (CInt(CaseId) + 1).ToString(), "", "", Session("USERNAME").ToString(), ModName)
            lbl.Text = Ds.Tables(0).Rows(0)("CASDES").ToString()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDescription:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCaseDescription(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal ModName As String, ByVal CaseId As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New VChainGetData.Selectdata()
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetCases(CaseId, (CInt(CaseId) + 1).ToString(), "", "", Session("USERNAME").ToString(), ModName)           
            LinkMat.Text = Ds.Tables(0).Rows(0)("CASDES").ToString()
            hid.Value = CaseId.ToString()          
            Path = "../Assumptions/Extrusion.aspx?ModName=" + ModName + "&CaseId=" + CaseId
            LinkMat.NavigateUrl = Path
            LinkMat.Target = "_blank"          
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDescription:" + ex.Message.ToString()
        End Try
    End Sub
End Class
