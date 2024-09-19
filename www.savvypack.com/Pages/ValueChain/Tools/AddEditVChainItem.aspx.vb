Imports System
Imports System.Data.OleDb
Imports System.Data

Partial Class Pages_ValueChain_Tools_AddEditVChainItem
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _GManagerButton As ImageButton
    Dim _btnLogOff As ImageButton
    Dim _ctlContentPlaceHolder As ContentPlaceHolder
    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property
    Public Property GManager() As ImageButton
        Get
            Return _GManagerButton
        End Get
        Set(ByVal Value As ImageButton)
            _GManagerButton = Value
        End Set
    End Property
    Public Property ctlContentPlaceHolder() As ContentPlaceHolder
        Get
            Return _ctlContentPlaceHolder
        End Get
        Set(ByVal value As ContentPlaceHolder)
            _ctlContentPlaceHolder = value
        End Set
    End Property
   
#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub
    Protected Sub GetGManagerButton()
        GManager = Page.Master.FindControl("imgGlobal")
        GManager.Visible = True
        AddHandler GManager.Click, AddressOf imgGlobal_Click
    End Sub
    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("VChainContentPlaceHolder")
    End Sub
    Protected Sub GetMasterPageControls()
        GetContentPlaceHolder()
        GetErrorLable()
        GetGManagerButton()
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            If Not IsPostBack Then
                Session("VChainCases") = Nothing
                BindValueChain()
                GetPages("")


            End If

        Catch ex As Exception
            ErrorLable.Text = "Page_Load:" + ex.Message
        End Try
    End Sub
    Protected Sub BindValueChain()
        Dim ds As New DataTable
        Dim objGetData As New VChainGetData.Selectdata
        Try
            Dim lst As New ListItem()
            lst.Text = "Nothing Selected"
            lst.Value = -1
            ddlVchain.Items.Clear()
            ddlVchain.Items.Add(lst)
            ddlVchain.AppendDataBoundItems = True

            ds = objGetData.GetValueChain("", Session("UserId").ToString())
            With ddlVchain
                .DataValueField = "VALUECHAINID"
                .DataTextField = "VALUECHAINNAME"
                .DataSource = ds
                .DataBind()
            End With

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlPageName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageName.SelectedIndexChanged
        'Dim dt As New DataTable
        'Dim objGetData As New VChainGetData.Selectdata
        'Try
        '    Session("VChainCases") = Nothing
        '    dt = objGetData.GetValueChain(ddlVchain.SelectedItem.Text, Session("UserId").ToString())
        '    GetCases(dt.Rows(0).Item("MODNAME").ToString(), dt.Rows(0).Item("RESULTCASES").ToString())

        'Catch ex As Exception

        'End Try
        Try
            BindDropDown()
            btnUpdate.Visible = False
            btnCancel.Visible = False
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindDropDown()
        Dim dt As New DataTable
        Dim objGetData As New VChainGetData.Selectdata
        Try
            Session("VChainCases") = Nothing
            dt = objGetData.GetValueChain(ddlVchain.SelectedItem.Text, Session("UserId").ToString())
            GetCases(dt.Rows(0).Item("MODNAME").ToString(), dt.Rows(0).Item("RESULTCASES").ToString())
           
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetCases(ByVal parMod As String, ByVal resCaseId As String)
        Dim ds As New DataSet
        Dim dsVChain As New DataSet
        Dim dsEchem As New DataSet
        Dim objGetData As New VChainGetData.Selectdata
        Dim i As New Integer
        Dim dsCount As New DataSet
        Dim CaseIds() As String
        Try
            If parMod.ToUpper() = "ECON2" Then
                If ddlPageName.SelectedItem.Text.ToUpper() = "ECON2" Then

                    Session("VChainCases") = resCaseId
                    GetVChainModCases("ECON2", "ECON2", resCaseId)

                ElseIf ddlPageName.SelectedItem.Text.ToUpper() = "ECON1" Then

                    'Getting E2 CaseId
                    Session("VChainCases") = resCaseId
                    GetVChainModCases("ECON2", "ECON2", resCaseId)

                    'Getting E1 CaseIds From E2
                    If Session("VChainCases") <> Nothing Then
                        CaseIds = Regex.Split(Session("VChainCases").ToString(), ",")
                        Session("VChainCases") = Nothing
                        For i = 0 To CaseIds.Length - 1
                            GetVChainModCases("ECON2", "ECON1", CaseIds(i))
                        Next
                    End If
                   

                    'Getting E1 CaseIds From E1
                    If Session("VChainCases") <> Nothing Then
                        CaseIds = Regex.Split(Session("VChainCases").ToString(), ",")
                        For i = 0 To CaseIds.Length - 1
                            GetVChainModCases("ECON1", "ECON1", CaseIds(i))
                        Next
                    End If
                   

                ElseIf ddlPageName.SelectedItem.Text.ToUpper() = "ECHEM1" Then

                    'Getting E2 CaseId
                    Session("VChainCases") = resCaseId
                    GetVChainModCases("ECON2", "ECON2", resCaseId)

                    'Getting E1 CaseIds From E2
                    If Session("VChainCases") <> Nothing Then
                        CaseIds = Regex.Split(Session("VChainCases").ToString(), ",")
                        Session("VChainCases") = Nothing
                        For i = 0 To CaseIds.Length - 1
                            GetVChainModCases("ECON2", "ECON1", CaseIds(i))
                        Next
                    End If
                 

                    'Getting E1 CaseIds From E1
                    If Session("VChainCases") <> Nothing Then
                        CaseIds = Regex.Split(Session("VChainCases").ToString(), ",")
                        For i = 0 To CaseIds.Length - 1
                            GetVChainModCases("ECON1", "ECON1", CaseIds(i))
                        Next
                    End If
                  

                    'Getting EChem1 CaseIds From E1
                    If Session("VChainCases") <> Nothing Then
                        CaseIds = Regex.Split(Session("VChainCases").ToString(), ",")
                        Session("VChainCases") = Nothing
                        For i = 0 To CaseIds.Length - 1
                            GetVChainModCases("ECON1", "ECHEM1", CaseIds(i))
                        Next
                    End If
                   

                    'Getting EChem1 CaseIds From Echem1
                    If Session("VChainCases") <> Nothing Then
                        CaseIds = Regex.Split(Session("VChainCases").ToString(), ",")
                        For i = 0 To CaseIds.Length - 1
                            GetVChainModCases("ECHEM1", "ECHEM1", CaseIds(i))
                        Next
                    End If
                  

                End If
            ElseIf parMod.ToUpper() = "ECON1" Then
                If ddlPageName.SelectedItem.Text.ToUpper() = "ECON1" Then

                    Session("VChainCases") = resCaseId
                    GetVChainModCases("ECON1", "ECON1", resCaseId)

                ElseIf ddlPageName.SelectedItem.Text.ToUpper() = "ECHEM1" Then

                    'Getting E1 CaseId
                    Session("VChainCases") = resCaseId
                    GetVChainModCases("ECON1", "ECON1", resCaseId)

                    'Getting EChem1 CaseIds From E1
                    If Session("VChainCases") <> Nothing Then
                        CaseIds = Regex.Split(Session("VChainCases").ToString(), ",")
                        Session("VChainCases") = Nothing
                        For i = 0 To CaseIds.Length - 1
                            GetVChainModCases("ECON1", "ECHEM1", CaseIds(i))
                        Next
                    End If
                    

                    'Getting EChem1 CaseIds From EChem1
                    If Session("VChainCases") <> Nothing Then
                        CaseIds = Regex.Split(Session("VChainCases").ToString(), ",")
                        For i = 0 To CaseIds.Length - 1
                            GetVChainModCases("ECHEM1", "ECHEM1", CaseIds(i))
                        Next
                    End If
                    
                End If
            ElseIf parMod.ToUpper() = "ECHEM1" Then

               If ddlPageName.SelectedItem.Text.ToUpper() = "ECHEM1" Then

                    'Getting Echem1 CaseId
                    Session("VChainCases") = resCaseId
                    GetVChainModCases("ECHEM1", "ECHEM1", resCaseId)
                End If
            End If

            'Getting Case Ids and Binding to dropdown
            ddlCaseId.Items.Clear()
            If ddlPageName.SelectedItem.Text.ToUpper() = "ECHEM11" And parMod.ToUpper() <> "ECHEM11" Then
                dsVChain = objGetData.GetVChainCases(Session("VChainCases"), ddlPageName.SelectedValue)
                If dsEchem.Tables(0).Rows.Count = 0 Then
                    Dim lst As New ListItem
                    lst.Text = "Nothing Selected"
                    lst.Value = 0
                    ddlCaseId.Items.Add(lst)
                Else
                    With ddlCaseId
                        .DataTextField = "CASEDES"
                        .DataValueField = "CASEID"
                        .DataSource = dsVChain
                        .DataBind()
                    End With
                End If
            Else
                dsVChain = objGetData.GetVChainCases(Session("VChainCases"), ddlPageName.SelectedValue)
                If dsVChain.Tables.Count = 0 Then
                    Dim lst As New ListItem
                    lst.Text = "Nothing Selected"
                    lst.Value = 0
                    ddlCaseId.Items.Add(lst)
                Else
                    With ddlCaseId
                        .DataTextField = "CASEDES"
                        .DataValueField = "CASEID"
                        .DataSource = dsVChain
                        .DataBind()
                    End With
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetVChainModCases(ByVal ParModName As String, ByVal modName As String, ByVal CaseId As String)
        Dim ds As New DataSet
        Dim objGetData As New VChainGetData.Selectdata
        Dim i As New Integer
        Dim dsCount As New DataSet
        Try
            ds = objGetData.GetVChainModCaseID(ParModName, modName, CaseId, ddlVchain.SelectedValue)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                If Session("VChainCases") Is Nothing Then
                    Session("VChainCases") = ds.Tables(0).Rows(i).Item("MODCASEID").ToString()
                    GetVChainModCases(ParModName, modName, ds.Tables(0).Rows(i).Item("MODCASEID").ToString())
                Else
                    dsCount = objGetData.CheckCases(ds.Tables(0).Rows(i).Item("MODCASEID").ToString(), Session("VChainCases").ToString())
                    If CInt(dsCount.Tables(0).Rows(0).Item("Count")) = 0 Then
                        Session("VChainCases") = Session("VChainCases").ToString() + "," + ds.Tables(0).Rows(i).Item("MODCASEID").ToString()
                        GetVChainModCases(ParModName, modName, ds.Tables(0).Rows(i).Item("MODCASEID").ToString())
                    End If

                End If

            Next

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub imgGlobal_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Response.Redirect("~/Pages/ValueChain/Default.aspx")
        Catch ex As Exception
            ErrorLable.Text = "imgGlobal_Click" + ex.Message
        End Try
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            trTable.Visible = True
            trButton.Visible = True
            btnUpdate.Visible = True
            btnCancel.Visible = True
            GetPageDetails()


        Catch ex As Exception
            ErrorLable.Text = "btnSubmit_Click" + ex.Message
        End Try
    End Sub
    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New VChainGetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim dt As New DataTable
        Dim message As String = String.Empty
        Try

            ds = objGetData.GetMaterialDetails(ddlVchain.SelectedValue.ToString(), ddlCaseId.SelectedValue, ddlPageName.SelectedItem.Text)

            For i = 1 To 4
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "40px", "Item", "1")
                        'HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "210px", "Material", "1")
                        'HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
               
                    Case 3
                        HeaderTdSetting(tdHeader, "210px", "Module Type", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Title = "Cases"
                        HeaderTdSetting(tdHeader, "250px", Title, "1")
                        'HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                End Select
            Next
            tblComp.Controls.Add(trHeader)
            'tblComp.Controls.Add(trHeader1)


            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 4
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Item
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "Label"
                            GetMaterialDetails(lbl, CInt(ds.Tables(0).Rows(i - 1).Item("MATID").ToString()))
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                          
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypModType" + i.ToString()
                            hid.ID = "hidModTypeId" + i.ToString()
                            Link.Width = 100
                            Link.CssClass = "Link"
                            GetModTypeDetails(Link, hid, CInt(ds.Tables(0).Rows(i - 1).Item("Modtype")), "hypCaseDes" + i.ToString(), "hidCaseId" + i.ToString())
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypCaseDes" + i.ToString()
                            hid.ID = "hidCaseId" + i.ToString()
                            Link.Width = 230
                            Link.CssClass = "Link"
                            GetCaseDetails(Link, hid, CInt(ds.Tables(0).Rows(i - 1).Item("Modcaseid").ToString()), CInt(ds.Tables(0).Rows(i - 1).Item("Modtype")), "hidModTypeId" + i.ToString())
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)


                    End Select
                Next

                trInner.CssClass = "AlterNateColor3"
                tblComp.Controls.Add(trInner)
            Next
        Catch ex As Exception
            ErrorLable.Text = "GetPageDetails" + ex.Message
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
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub GetModTypeDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal typeId As Integer, ByVal strCaseLink As String, ByVal strCaseId As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New VChainGetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetModuleType(typeId.ToString(), "")

            LinkMat.Text = Ds.Tables(0).Rows(0).Item("DES").ToString()
            LinkMat.ToolTip = Ds.Tables(0).Rows(0).Item("DES").ToString()
            LinkMat.Attributes.Add("text-decoration", "none")
            hid.Value = Ds.Tables(0).Rows(0).Item("typeId").ToString() 'CaseIdE1.ToString()
            Path = "../PopUp/GetModType.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&CaseDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + strCaseLink + "&CaseId=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + strCaseId + ""
            LinkMat.NavigateUrl = "javascript:ShowModPopWindow('" + Path + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetMaterialDetails(ByRef lbl As Label, ByVal MatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New VChainGetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetMaterials(MatId, "", "", ddlPageName.SelectedValue)
            lbl.Text = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
          
        Catch ex As Exception
            ErrorLable.Text = "Error:GetMaterialDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetCaseDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal CaseIdE1 As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New VChainGetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim schema As String = String.Empty
        Try
            If ddlPageName.SelectedItem.Text = "Econ2" Then
                schema = "EconConnection"
            ElseIf ddlPageName.SelectedItem.Text = "Econ1" Then
                schema = "EconConnection"
            End If
            Ds = ObjGetdata.GetCases(CaseIdE1, ddlCaseId.SelectedValue, "", "", Session("VChainUserName").ToString(), schema)

            If Ds.Tables(0).Rows.Count = 0 Then
                LinkMat.Text = "Conflict"
                LinkMat.Attributes.Add("text-decoration", "none")
            Else
                LinkMat.Text = Ds.Tables(0).Rows(0).Item("CASDES").ToString()
                LinkMat.ToolTip = Ds.Tables(0).Rows(0).Item("CASDES").ToString()
                LinkMat.Attributes.Add("text-decoration", "none")
            End If

            hid.Value = CaseIdE1.ToString()
            Path = "../PopUp/GetCasePopup.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&CaseId=" + ddlCaseId.SelectedValue + "&Schm=" + schema + " "
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:GetCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetCaseDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal CaseIdE1 As Integer, ByVal modId As String, ByVal modTypeId As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New VChainGetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim schema As String = String.Empty
        Try

            If ddlPageName.SelectedItem.Text = "Econ2" Then
                schema = "EconConnectionString"
            ElseIf ddlPageName.SelectedItem.Text = "Econ1" Then
                schema = "EconConnectionString"
            End If

            Ds = ObjGetdata.GetModuleCases(CaseIdE1, ddlCaseId.SelectedValue, "", "", Session("VChainUserName").ToString(), modId)

            If Ds.Tables(0).Rows.Count = 0 Then
                LinkMat.Text = "Conflict"
                LinkMat.Attributes.Add("text-decoration", "none")
            Else
                LinkMat.Text = Ds.Tables(0).Rows(0).Item("CASDES").ToString()
                LinkMat.ToolTip = Ds.Tables(0).Rows(0).Item("CASDES").ToString()
                LinkMat.Attributes.Add("text-decoration", "none")
            End If

            hid.Value = CaseIdE1.ToString()
            'Path = "../PopUp/GetCasePopup.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID
            Path = "../PopUp/GetCasePopup.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&CaseId=" + ddlCaseId.SelectedValue + "&Schm=" + schema + " "
            LinkMat.NavigateUrl = "javascript:ShowModPopWindow('" + Path + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_" + Regex.Replace(hid.ClientID, "Case", "ModType") + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim Cases(9) As String
        Dim ModType(9) As String
        Dim Price(9) As String
        Dim i As New Integer
        Dim objUpdate As New VChainUpInsData.UpdateInsert()
        Try
            For i = 1 To 10
                ModType(i - 1) = Request.Form("ctl00$VChainContentPlaceHolder$hidModTypeId" + i.ToString() + "")
                Cases(i - 1) = Request.Form("ctl00$VChainContentPlaceHolder$hidCaseId" + i.ToString() + "")
            Next
            objUpdate.CaseUpdate(ddlVchain.SelectedValue.ToString(), ddlCaseId.SelectedValue, ModType, Cases, ddlPageName.SelectedValue, ddlPageName.SelectedItem.Text, Price, "Y")
            'trButton.Visible = False
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('ValueChain Items added successfully');", True)
            GetPageDetails()
            BindDropDown()
        Catch ex As Exception
            ErrorLable.Text = "btnUpdate_Click" + ex.Message
        End Try
    End Sub
    Protected Sub GetPages(ByVal PageName As String)
        Dim ds1 As New DataSet
        Dim ds2 As New DataSet
        Dim objGetData As New VChainGetData.Selectdata
        Try

            ds1 = objGetData.GetPages(PageName)
            With ddlPageName
                .DataTextField = "PAGE"
                .DataValueField = "VALUE"
                .DataSource = ds1
                .DataBind()
            End With

           

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlVchain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVchain.SelectedIndexChanged
        Dim dt As New DataTable
        Dim objGetData As New VChainGetData.Selectdata
        Try
            dt = objGetData.GetValueChain(ddlVchain.SelectedItem.Text, Session("UserId").ToString())
            If dt.Rows.Count > 0 Then
                GetPages(dt.Rows(0).Item("ModName").ToString())
                GetCases(dt.Rows(0).Item("MODNAME").ToString(), dt.Rows(0).Item("RESULTCASES").ToString())
            Else
                GetPages("")
                GetCases("", "")
            End If
           

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            trButton.Visible = False
        Catch ex As Exception
            ErrorLable.Text = "btnCancel_Click" + ex.Message
        End Try
    End Sub
End Class
