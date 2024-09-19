Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginGetData
Imports LoginUpdateData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Universal_loginN_Pages_CorpUser
    Inherits System.Web.UI.Page
#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_Universal_loginN_Pages_CorpUser")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
        'GetPageDetails()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            If Not IsPostBack Then
           
                GetPageDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim dsEffCountry As New DataSet
        Dim objGetData As New LoginGetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim btnLink As New LinkButton
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Try
            ds = objGetData.GetAdministratorUserDetails(Session("CCOMPANY").ToString().Replace("'", "''"))

            If ds.Tables(0).Rows.Count > 0 Then
                'Header Section
                For i = 1 To 6
                    tdHeader = New TableCell
                    tdHeader1 = New TableCell
                    tdHeader2 = New TableCell
                    Dim Title As String = String.Empty
                    'Header
                    Select Case i
                        Case 1
                            HeaderTdSetting(tdHeader, "25px", "", "1")
                            trHeader.Controls.Add(tdHeader)
                        Case 2
                            HeaderTdSetting(tdHeader, "235px", "User Name", "1")
                            trHeader.Controls.Add(tdHeader)
                        Case 3
                            HeaderTdSetting(tdHeader, "130px", "Password", "1")
                            trHeader.Controls.Add(tdHeader)
                        Case 4
                            HeaderTdSetting(tdHeader, "120px", "Company", "1")
                            trHeader.Controls.Add(tdHeader)
                        Case 5
                            HeaderTdSetting(tdHeader, "120px", "Participant in Corporate License", "1")
                            trHeader.Controls.Add(tdHeader)
                        Case 6
                            HeaderTdSetting(tdHeader, "90px", "Corporate License", "1")
                            trHeader.Controls.Add(tdHeader)

                    End Select
                Next
                trHeader.Height = 30
                tblComparision.Controls.Add(trHeader)

                'Inner
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    trInner = New TableRow
                    For j = 1 To 6
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "26px", "Center")
                                tdInner.Text = "<b>" + (i + 1).ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "230px", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypPosDes" + i.ToString()
                                hid.ID = "hidPosId" + i.ToString()
                                Link.CssClass = "Link"
                                Link.Text = ds.Tables(0).Rows(i).Item("USERNAME").ToString()
                                Link.NavigateUrl = "AddEditUser.aspx?ID=" + ds.Tables(0).Rows(i).Item("USERID").ToString() + "&Type=Edit"
                                ' Link.NavigateUrl = "javascript:ShowEditWindow(" + ds.Tables(0).Rows(i).Item("USERID").ToString() + ")"

                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)

                            Case 3
                                InnerTdSetting(tdInner, "127px", "Left")
                                lbl = New Label
                                If (ds.Tables(0).Rows(i).Item("Password")).ToString() <> "" Then
                                    lbl.Text = ds.Tables(0).Rows(i).Item("Password").ToString()
                                Else
                                    lbl.Text = ""
                                End If
                                'lbl.CssClass = "NormalLabel"
                                lbl.Width = 70
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "115px", "Left")
                                lbl = New Label
                                If (ds.Tables(0).Rows(i).Item("Company")).ToString() <> "" Then
                                    lbl.Text = ds.Tables(0).Rows(i).Item("Company").ToString()
                                Else
                                    lbl.Text = ""
                                End If
                                'lbl.CssClass = "NormalLabel"
                                lbl.Width = 90
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case 5
                                InnerTdSetting(tdInner, "115px", "Left")
                                lbl = New Label
                                If (ds.Tables(0).Rows(i).Item("ISCORPORATEUSERT")).ToString() <> "" Then
                                    lbl.Text = ds.Tables(0).Rows(i).Item("ISCORPORATEUSERT").ToString()
                                Else
                                    lbl.Text = ""
                                End If
                                'lbl.CssClass = "NormalLabel"
                                lbl.Width = 90
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "85px", "Left")
                                ' Link = New HyperLink
                                Dim id As String
                                btnLink = New LinkButton
                                hid = New HiddenField
                                btnLink.ID = "hypPosDes" + i.ToString()
                                id = btnLink.ID.ToString()
                                hid.ID = "hidPosId" + i.ToString()
                                Dim userId As String
                                btnLink.Width = 70
                                userId = ds.Tables(0).Rows(i).Item("USERID").ToString()

                                btnLink.CssClass = "Link"
                                If ds.Tables(0).Rows(i).Item("ISCORPORATEUSERF").ToString().ToUpper() = "Y" Then
                                    btnLink.Text = "Remove"
                                Else
                                    btnLink.Text = "Add"
                                End If
                                btnLink.OnClientClick = "javascript:return AddRemove('" + btnLink.Text + "','" + userId + "');"
                                'btnAddRemove_Click(btnLink)
                                ' Link.NavigateUrl = "javascript:ShowEditWindow('" + hid.Value + "')"

                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(btnLink)
                                trInner.Controls.Add(tdInner)


                        End Select
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    trInner.Height = 23
                    tblComparisionInner.Controls.Add(trInner)
                Next

            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPageDetails:" + ex.Message.ToString()
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

    Protected Sub btnAddRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRemove.Click
        Dim str As String
        Dim objGetData As New LoginGetData.Selectdata
        Dim objUpdateData As New LoginUpdateData.Selectdata
        Dim obj As New CryptoHelper
        Dim ds As New DataSet
        Dim userId As String
        Dim userCount As Integer = 0
        Dim userLimit As Integer = 0
        Dim CUserId As String
        Try
            If Not objRefresh.IsRefresh Then
                userId = hfUserId.Value.ToString()
                str = hfBtnText.Value.ToString()
                userCount = objGetData.GetUserCount(userId)
                userLimit = objGetData.GetMaxCarporateUsers(Session("CUserId").ToString())
                CUserId = Session("CUserId").ToString()

                If str = "Add" Then
                    If userCount >= userLimit Then
                        Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE116").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    Else
                        objUpdateData.InsertUserPermissions(userId, CUserId)
                    End If

                ElseIf str = "Remove" Then
                    objUpdateData.UpdateUser(userId)

                End If
            End If
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub btnLogOff_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLogOff.Click
        'Session("UserName") = ""
        'Session("Password") = ""
        'Session("UserId") = ""
        'Session("CUserName") = ""
        'Session("CPassword") = ""
        'Session("CUserId") = ""
        'Session("CCOMPANY") = ""
        'Session("back") = ""
        'Session("LicenseNo") = ""
        Session.Abandon()
        Session.RemoveAll()
        Response.Redirect("~/Index.aspx", True)
    End Sub
End Class
