Imports System.Data
Imports StudyGetData
Imports SiteGetData
Partial Class Masters_AlliedMasterM
    Inherits System.Web.UI.MasterPage


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objGetData As New UsersGetData.Selectdata()
        'Dim UserLogin_Path As String = System.Configuration.ConfigurationManager.AppSettings("UserLogin_Path")
        Dim UserLogin_Path As String = ""
        'If HttpContext.Current.Request.IsSecureConnection = True Then
        '    UserLogin_Path = System.Configuration.ConfigurationManager.AppSettings("UserLogin_PathS")
        'Else
        '    UserLogin_Path = System.Configuration.ConfigurationManager.AppSettings("UserLogin_Path")
        'End If
        Dim currenturl = Request.ServerVariables("HTTP_HOST")
        UserLogin_Path = "http://" + currenturl + System.Configuration.ConfigurationManager.AppSettings("UserLogin_Path1")
        If currenturl.Contains("www.savvypack.com") Then
            If HttpContext.Current.Request.IsSecureConnection.Equals(False) Then
                Response.Redirect(Request.Url.ToString().Replace("http://", "https://"))
            End If
        End If
        Dim ds As New DataSet()
        Try
            If Not IsPostBack Then
                Session("Savvy") = "N"
            Else
                If Session("Savvy") = "Y" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "window.open('OnlineForm/ProjectManager.aspx','_blank');", True)
                End If
                If hdnAlert.Value <> "0" Then
                    hdnAlert.Value = "0"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Thank you for creating your account. Your email address has been verified, and you can now use your account.');", True)
                End If
            End If

            If UserLogin_Path <> "" Then
                hdnUserLoginPath.Value = UserLogin_Path
            End If

            If Request.UserAgent.IndexOf("AppleWebKit") > 0 Then
                Request.Browser.Adapters.Clear()
            End If
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Allied Development Corp."
            Catch ex As Exception
            End Try

            lnkLogout.Attributes.Add("onclick", "return OpenLoginPopup();")
            If Session("MenuItem") = "ACNT" Or Session("MenuItem") = "DWNLD" Then
                If Session("UserId") = Nothing Then
                    lnkLogout.Enabled = False
                    lnkLogout.ForeColor = Drawing.Color.DarkGray
                    lnkLogout.Attributes.Add("onclick", "return functionNone();")
                Else
                    lnkLogout.Enabled = True
                End If

            End If

            If Session("RefNumber") = Nothing Then
                Session("RefNumber") = ""
            End If
            If Session("WRefNumber") = Nothing Then
                Session("WRefNumber") = ""
            End If
            If Session("UserId") = Nothing Then
                lnkLogout.Text = "Login"
                AlliedMenu2.FindItem("SHOPPING").Enabled = False
                AlliedMenu.FindItem("ACNT").Enabled = False
                AlliedMenu.FindItem("SAVVYPACK").Enabled = False
                AlliedMenu2.FindItem("DWNLD").Enabled = False
                AlliedMenu.FindItem("SAVVYPACK").ToolTip = "Please LogIn to access this service"
                AlliedMenu2.FindItem("DWNLD").ToolTip = "Please LogIn to access this service"
                AlliedMenu.FindItem("ACNT").ToolTip = "Please LogIn to access this service"
            Else
                AlliedMenu.FindItem("SAVVYPACK").ToolTip = ""
                AlliedMenu2.FindItem("DWNLD").ToolTip = ""
                AlliedMenu.FindItem("ACNT").ToolTip = ""
                AlliedMenu.FindItem("ACNT").Enabled = True
                AlliedMenu.FindItem("ACNT").Text = "Account Management"
                AlliedMenu.FindItem("ACNT").ImageUrl = ""

                AlliedMenu.FindItem("SAVVYPACK").Enabled = True
                AlliedMenu.FindItem("SAVVYPACK").Text = "SavvyPack®"
                AlliedMenu.FindItem("SAVVYPACK").ImageUrl = ""

                AlliedMenu2.FindItem("DWNLD").Enabled = True
                AlliedMenu2.FindItem("DWNLD").Text = "Download"
                AlliedMenu2.FindItem("DWNLD").ImageUrl = ""

                lnkLogout.Text = "Logout"
                truser.Visible = True
                ds = objGetData.GetUserDetails(Session("UserId").ToString())
                If ds.Tables(0).Rows.Count > 0 Then
                    lblUserMess.Text = "Welcome <span style='color:#7c1a27;font-weight:normal;font-size:12px;'>" + ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString() + " " + ds.Tables(0).Rows(0).Item("LASTNAME").ToString() + "</span>"
                End If

                If IsShop() Then
                    AlliedMenu2.FindItem("SHOPPING").Enabled = True
                    AlliedMenu2.FindItem("SHOPPING").Text = "Shopping Cart"
                    AlliedMenu2.FindItem("SHOPPING").ImageUrl = ""
                Else
                    AlliedMenu2.FindItem("SHOPPING").Enabled = False
                End If
            End If

            If Session("https") = Nothing Then

            Else

                Session("https") = Nothing
                Response.Redirect("https://www.allied-dev.com/", True)
            End If

            GetLeftMenu()

            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoServerCaching()
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
            Response.Cache.SetNoStore()
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(30))
            Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate)
            Response.Cache.SetValidUntilExpires(True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Sub AddedControl(ByVal control As Control, ByVal index As Integer)
        If Request.ServerVariables("http_user_agent").IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) <> -1 Then
            Me.Page.ClientTarget = "uplevel"
        End If
        MyBase.AddedControl(control, index)
    End Sub

    Protected Sub AlliedMenu_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles AlliedMenu.MenuItemClick
        Try

            SetMenu(e.Item.Value)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub AlliedMenu2_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles AlliedMenu2.MenuItemClick
        Try

            SetMenu(e.Item.Value)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SetMenu(ByVal Item As String)
        Try

            Session("MenuItem") = Item
            Select Case Item
                Case "HM"
                    Response.Redirect("~/Index.aspx")
                Case "PRDCTSER"
                    Response.Redirect("~/ProductService.aspx")
                Case "CONSULT"
                    Response.Redirect("~/Consulting/Consulting.aspx")
                Case "MCB"
                    Response.Redirect("~/Studies/multiclientstudy.aspx")
                Case "ISERVICE"
                    Response.Redirect("~/InteractiveServices/InteractiveServices.aspx")
                Case "PDESIGN"
                    Response.Redirect("~/PackageDesign.aspx")
                Case "WEBCONF"
                    Response.Redirect("~/WebConferenceN/WebConfHome.aspx")
                Case "JWEBCONF"
                    Response.Redirect("~/WebConferenceN/JoinWebconf.aspx")
                Case "RWEBCONF"
                    Response.Redirect("~/WebConferenceN/WebConf.aspx")
                Case "ACNT"
                    Response.Redirect("~/Users_Login/Login.aspx")
                Case "SAVVYPACK"
                    Response.Redirect("~/Universal_loginN/Pages/MemberLogin.aspx")
                Case "ABTUS"
                    Response.Redirect("~/AboutUs.aspx")
                Case "CNCTUS"
                    Response.Redirect("~/ContactUsN.aspx")
                Case "SHOPPING"
                    'Response.Redirect("~/ShoppingCart/OrderReview.aspx")
                Case "APPDEV"
                    Response.Redirect("~/ApplicationDevelopment/AppDev.aspx")
                Case "DWNLD"
                    Response.Redirect("~/DownLoad/Login.aspx")
            End Select
        Catch ex As Exception '
            lblError.Text = "Error:AlliedMaster:SetMenu" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetLeftMenu()
        Try
            If Session("MenuItem") <> Nothing Then
                Select Case Session("MenuItem")
                    Case "CONSULT"
                        GetConsulting()
                    Case "MCB"
                        GetPublication()
                    Case "ISERVICE"
                        GetIntractive()
                    Case "PDESIGN"
                        GetProductAndService()
                    Case "WEBCONF", "JWEBCONF", "RWEBCONF"
                        GetWebConf()
                    Case "PRDCTSER"
                        GetProductAndService()
                    Case "HM", "ABTUS"
                        GetHome()
                    Case "APPDEV"
                        GetApplicationDev()
                    Case "SAISERVICE"
                        GetSAIntractive()
                    Case Else
                        GetClear()
                End Select


            End If
        Catch ex As Exception
            lblError.Text = "Error:Masters_AlliedMaster:GetLeftMenu" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetConsulting()
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim i As New Integer
        Dim hypLink As New HyperLink()
        Dim ds As New DataSet
        Dim objGetData As New SiteGetData.Selectdata()
        Dim objCrypto As New CryptoHelper()
        Try
            GetClear()
            'ds = objGetData.GetServicesLinks()
            For i = 0 To 4
                tr = New TableRow
                td = New TableCell
                hypLink = New HyperLink
                td.CssClass = "SubMenu"
                hypLink.CssClass = "LinkSubMenu"
                Select Case i
                    Case 0
                        hypLink.Text = "CONSULTING"
                        hypLink.NavigateUrl = "~/Consulting/Consulting.aspx"
                    Case 1
                        hypLink.Text = "<div style='margin-left:10px;'>Analytical Research</div>"
                        hypLink.NavigateUrl = "~/Consulting/Analytical.aspx"
                    Case 2
                        hypLink.Text = "<div style='margin-left:10px;'>Business Research</div>"
                        hypLink.NavigateUrl = "~/Consulting/Business.aspx"
                    Case 3
                        hypLink.Text = "<div style='margin-left:10px;'>Program Development & Implementation</div>"
                        hypLink.NavigateUrl = "~/Consulting/ProgramDev.aspx"
                    Case 4
                        hypLink.Text = "<div style='margin-left:10px;'>Allied Development Advantages</div>"
                        hypLink.NavigateUrl = "~/Consulting/Advantages.aspx"


                        'hypLink.Text = "<div style='margin-left:10px;'>" + ds.Tables(0).Rows(i).Item("PAGENAME").ToString() + "</div>"
                        'hypLink.NavigateUrl = "~/ServicesN/" + ds.Tables(0).Rows(i).Item("PAGENAME").ToString().Trim() + ".aspx"
                End Select
                td.Controls.Add(hypLink)
                tr.Controls.Add(td)
                tblLeftMenu.Controls.Add(tr)
            Next

        Catch ex As Exception
            lblError.Text = "Error:Masters_AlliedMaster:GetConsulting" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPublication()
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim i As New Integer
        Dim hypLink As New HyperLink()
        Dim ds As New DataSet
        Dim objGetData As New StudyGetData.Selectdata()
        Dim objCrypto As New CryptoHelper()
        Try
            ds = objGetData.GetStudyDetails("")
            GetClear()
            For i = -1 To ds.Tables(0).Rows.Count - 1
                tr = New TableRow
                td = New TableCell
                hypLink = New HyperLink
                td.CssClass = "SubMenu"
                hypLink.CssClass = "LinkSubMenu"
                Select Case i
                    Case -1
                        hypLink.Text = "PUBLICATIONS"
                        hypLink.NavigateUrl = "~/Studies/multiclientstudy.aspx"
                    Case Else
                        hypLink.Text = "<div style='margin-left:10px;'>" + ds.Tables(0).Rows(i).Item("SHORTTITLE").ToString() + "</div>"
                        'hypLink.NavigateUrl = "~/Studies/StudyDetails.aspx?ID=" + objCrypto.Encrypt(ds.Tables(0).Rows(i).Item("ReportID").ToString()) + " "
                        hypLink.NavigateUrl = "~/Studies/" + ds.Tables(0).Rows(i).Item("SHORTTITLE").ToString().Replace(" ", "_").Replace(".", "") + "_Details.aspx"
                        hypLink.NavigateUrl = hypLink.NavigateUrl.Replace("__", "_")
                End Select
                td.Controls.Add(hypLink)
                tr.Controls.Add(td)
                tblLeftMenu.Controls.Add(tr)
            Next
        Catch ex As Exception
            lblError.Text = "Error:Masters_AlliedMaster:GetProductAndServices" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetIntractive()
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim i As New Integer
        Dim hypLink As New HyperLink()
        Try

            GetClear()
            For i = 1 To 12
                tr = New TableRow
                td = New TableCell
                hypLink = New HyperLink
                td.CssClass = "SubMenu"
                hypLink.CssClass = "LinkSubMenu"
                Select Case i
                    Case 1
                        hypLink.Text = "INTERACTIVE SERVICES"
                        hypLink.NavigateUrl = "~/InteractiveServices/InteractiveServices.aspx"
                    Case 2
                        hypLink.Text = "<div style='margin-left:10px;'>Structure Assistant</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/SAModule.aspx"
                    Case 3
                        hypLink.Text = "<div style='margin-left:10px;'>Packaging Analysis</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/PackAnalysisService.aspx"
                    Case 4
                        hypLink.Text = "<div style='margin-left:20px;'>Econ1</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/EcoModule1.aspx"
                    Case 5
                        hypLink.Text = "<div style='margin-left:20px;'>Econ2</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/EcoModule2.aspx"
                    Case 6
                        hypLink.Text = "<div style='margin-left:20px;'>Econ3</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/Econ3Module.aspx"
                    Case 7
                        hypLink.Text = "<div style='margin-left:20px;'>Sustain1</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/EnvModule1.aspx"
                    Case 8
                        hypLink.Text = "<div style='margin-left:20px;'>Sustain2</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/EnvModule2.aspx"
                    Case 9
                        hypLink.Text = "<div style='margin-left:20px;'>Sustain3</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/Sustain3Module.aspx"
                    Case 10
                        hypLink.Text = "<div style='margin-left:10px;'>Knowledgebases</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/InteractiveKb.aspx"
                    Case 11
                        hypLink.Text = "<div style='padding-left:10px;'>On-line Research</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/OnlineResearch.aspx"
                    Case 12
                        hypLink.Text = "<div style='padding-left:10px;'>SEIC</div>"
                        hypLink.NavigateUrl = "~/Pages/Dow/SEICLogin.aspx"

                End Select
                td.Controls.Add(hypLink)
                tr.Controls.Add(td)
                tblLeftMenu.Controls.Add(tr)
            Next

        Catch ex As Exception
            lblError.Text = "Error:Masters_AlliedMaster:GetProductAndServices" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetWebConf()
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim i As New Integer
        Dim hypLink As New HyperLink()
        Try

            GetClear()
            For i = 1 To 3
                tr = New TableRow
                td = New TableCell
                hypLink = New HyperLink
                td.CssClass = "SubMenu"
                hypLink.CssClass = "LinkSubMenu"
                Select Case i
                    Case 1
                        hypLink.Text = "WEB CONFERENCE"
                        hypLink.NavigateUrl = "~/WebConferenceN/WebConfHome.aspx"
                    Case 2
                        hypLink.Text = "<div style='margin-left:10px;'>Join Conference</div>"
                        hypLink.NavigateUrl = "~/WebConferenceN/JoinWebconf.aspx"
                    Case 3
                        hypLink.Text = "<div style='margin-left:10px;'>Register for a Conference</div>"
                        hypLink.NavigateUrl = "~/WebConferenceN/WebConf.aspx"
                End Select
                td.Controls.Add(hypLink)
                tr.Controls.Add(td)
                tblLeftMenu.Controls.Add(tr)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetProductAndService()
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim i As New Integer
        Dim hypLink As New HyperLink()
        Try

            GetClear()
            For i = 1 To 6
                tr = New TableRow
                td = New TableCell
                hypLink = New HyperLink
                td.CssClass = "SubMenu"
                hypLink.CssClass = "LinkSubMenu"
                Select Case i
                    Case 1
                        hypLink.Text = "PRODUCTS & SERVICES"
                        hypLink.NavigateUrl = "~/ProductService.aspx"
                    Case 2
                        hypLink.Text = "<div style='margin-left:10px;'>Consulting</div>"
                        hypLink.NavigateUrl = "~/Consulting/Consulting.aspx"
                    Case 3
                        hypLink.Text = "<div style='margin-left:10px;'>Publications</div>"
                        hypLink.NavigateUrl = "~/Studies/multiclientstudy.aspx"
                    Case 4
                        hypLink.Text = "<div style='margin-left:10px;'>Interactive Services</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/InteractiveServices.aspx"
                    Case 5
                        hypLink.Text = "<div style='margin-left:10px;'>Package Design (Structural)</div>"
                        hypLink.NavigateUrl = "~/PackageDesign.aspx"
                    Case 6
                        hypLink.Text = "<div style='margin-left:10px;'>Application Development</div>"
                        hypLink.NavigateUrl = "~/ApplicationDevelopment/AppDev.aspx"
                End Select
                td.Controls.Add(hypLink)
                tr.Controls.Add(td)
                tblLeftMenu.Controls.Add(tr)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetHome()
        Dim objGetData As New SiteGetData.Selectdata()
        Dim objGetData2 As New StudyGetData.Selectdata()
        Dim obj As New StringBuilder()
        Dim ds As New DataSet
        Dim dsReports As New DataSet

        Try
            tblLeftMenu.Rows.Clear()
            ds = objGetData.GetClients()
            dsReports = objGetData2.GetStudyDetails("")
            IsBSManage.Visible = True
            IsSP.Visible = True

            obj.Append("<table cellpadding=""0"" cellspacing=""0"" class=""newsticker-table"" Width=""103%""><tr><td class=""SubMenu"" style=""margin-left:5px"">CLIENTS..</td></tr>")
            obj.Append("<tr><td><div class=""newsticker-jcarousellite"">")
            obj.Append("<ul>")
            For Each dr As DataRow In ds.Tables(0).Rows
                obj.Append("<li><div class=""info"">" + dr("Name").ToString() + "</div></li>")
            Next
            obj.Append("</ul>")
            obj.Append("</div></td></tr></table>")
            clientsticker.InnerHtml = obj.ToString()

            obj = New StringBuilder()
            obj.Append("<table cellpadding=""0"" cellspacing=""0"" class=""newsticker-table"" Width=""103%""><tr><td class=""SubMenu"" style=""margin-left:5px"">PUBLICATIONS..</td></tr>")
            obj.Append("<tr><td><div class=""newsticker-jcarousellite"">")
            obj.Append("<ul>")
            For Each dr As DataRow In dsReports.Tables(0).Rows
                obj.Append("<li><div class=""info"">" + dr("SHORTTITLE").ToString() + "</div></li>")
            Next
            obj.Append("</ul>")
            obj.Append("</div></td></tr></table>")


            Publicationsticker.InnerHtml = obj.ToString()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetClear()
        Try
            tblLeftMenu.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Protected Function IsShop() As Boolean
        Dim ds As New DataSet
        Dim objGetData As New ShopGetData.Selectdata()
        Try
            ds = objGetData.GetOrderReview(Session("RefNumber"))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function


    Protected Sub GetApplicationDev()
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim i As New Integer
        Dim hypLink As New HyperLink()
        Try

            GetClear()
            For i = 1 To 4
                tr = New TableRow
                td = New TableCell
                hypLink = New HyperLink
                td.CssClass = "SubMenu"
                hypLink.CssClass = "LinkSubMenu"
                Select Case i
                    Case 1
                        hypLink.Text = "APPLICATION DEVELOPMENT"
                        hypLink.NavigateUrl = "~/ApplicationDevelopment/AppDev.aspx"
                    Case 2
                        hypLink.Text = "<div style='margin-left:10px;'>Services</div>"
                        hypLink.NavigateUrl = "~/ApplicationDevelopment/Services.aspx"
                    Case 3
                        hypLink.Text = "<div style='margin-left:10px;'>Benefits</div>"
                        hypLink.NavigateUrl = "~/ApplicationDevelopment/Benefits.aspx"
                    Case 4
                        hypLink.Text = "<div style='margin-left:10px;'>Examples</div>"
                        hypLink.NavigateUrl = "~/ApplicationDevelopment/Examples.aspx"
                End Select
                td.Controls.Add(hypLink)
                tr.Controls.Add(td)
                tblLeftMenu.Controls.Add(tr)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click
        Try
            Dim objUpdate As New LoginUpdateData.Selectdata
            If Session("TID") <> Nothing Then
                'objUpdate.UpdateLogOffDetails2(Session("UserName"), Session("TID"), Session.SessionID)
                If Session("LogInCount") <> Nothing Then
                    objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), Session("LogInCount").ToString())
                Else
                    objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), "")

                End If

            End If
            Session.Abandon()
            Session.RemoveAll()
            Response.Redirect("~/Index.aspx", True)


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetSAIntractive()
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim f As New Integer
        Dim hypLink As New HyperLink()
        Dim btnSponsor As New Button()
        Dim lblSponsor As New Label()
        Dim ImgBut As New ImageButton
        Dim ds As New DataSet
        Dim objGetData As New StandGetData.Selectdata()
        Dim Path As String = String.Empty
        Dim dsSessions As New DataSet
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim lbl As New Label
        Dim FlagSupId As String = String.Empty
        Dim FlagSupImg As String = String.Empty
        Dim hid As New HiddenField
        Dim hidGrade As New HiddenField
        Dim hidDes As New HiddenField
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim objUpIns As New StandUpInsData.UpdateInsert()
        Dim arrFlag() As String
        Dim arrSponFlag() As String
        Dim dvFlag As New DataView
        Dim dtFlag As New DataTable
        Try

            GetClear()
            ds = objGetData.GetFlagSponsors()
            For j = 1 To 1
                tr = New TableRow
                td = New TableCell
                hypLink = New HyperLink
                btnSponsor = New Button

                btnSponsor.CssClass = "ButtonWMarigin"
                Select Case j
                    Case 1

                        btnSponsor.Text = "Information On Sponsorship"
                        Path = "../InteractiveServices/InfoOnSponsor.aspx"
                        btnSponsor.Attributes.Add("onclick", "window.open('" + Path + "'); return false;")
                        btnSponsor.Width = 170
                End Select

                td.Controls.Add(btnSponsor)
                tr.Controls.Add(td)
                tblSponsor.Controls.Add(tr)
            Next
            For k = 1 To 1
                tr = New TableRow
                td = New TableCell
                lblSponsor = New Label
                Select Case k
                    Case 1
                        lblSponsor.Text = "<div style='margin-left:10px;padding-top:5px;font-weight:bold;color:Black;font-family:Verdana;font-size:13px;'>Sponsored By:</div>"
                End Select
                td.Controls.Add(lblSponsor)
                tr.Controls.Add(td)
                tblSponsor.Controls.Add(tr)
            Next
            dsSessions = objGetData.GetFlagTempQuery(Session.SessionID)
            If dsSessions.Tables(0).Rows.Count > 0 Then
                arrFlag = Regex.Split(dsSessions.Tables(0).Rows(0).Item("SUPPLIERIDS").ToString(), ",")
                arrSponFlag = Regex.Split(dsSessions.Tables(0).Rows(0).Item("SPONSORFLAGS").ToString(), ",")
                ds = objGetData.GetFlagSponsors()
                'ds = Session("dsFlags")
                dvFlag = ds.Tables(0).DefaultView
                For i = 0 To ds.Tables(0).Rows.Count + 1
                    trInner = New TableRow
                    For j = 1 To 2
                        tdInner = New TableCell
                        If i < ds.Tables(0).Rows.Count + 1 Then
                            If i > 0 Then
                                Select Case j
                                    Case 1
                                        InnerTdSetting(tdInner, "", "Left")
                                        ImgBut = New ImageButton
                                        dvFlag.RowFilter = "SUPPLIERID =" + arrFlag(i - 1) + " AND IMAGE='" + arrSponFlag(i - 1) + "'"
                                        dtFlag = dvFlag.ToTable()

                                        ImgBut.ID = "imgBut" + i.ToString()
                                        ImgBut.Width = 80
                                        ImgBut.Height = 53.5
                                        ImgBut.CommandArgument = dtFlag.Rows(0).Item("SUPPLIERID").ToString()
                                        ImgBut.ImageUrl = "~/Images/" + dtFlag.Rows(0).Item("IMAGE").ToString() + ""

                                        ImgBut.Attributes.Add("onclick", "window.open('" + dtFlag.Rows(0).Item("IMAGEURL").ToString() + "'); return false;")
                                        AddHandler ImgBut.Click, AddressOf ImgBut_Click
                                        tdInner.Controls.Add(ImgBut)
                                        trInner.Controls.Add(tdInner)
                                        i = i + 1

                                    Case 2
                                        InnerTdSetting(tdInner, "", "Left")
                                        ImgBut = New ImageButton
                                        dvFlag.RowFilter = "SUPPLIERID =" + arrFlag(i - 1) + " AND IMAGE='" + arrSponFlag(i - 1) + "'"
                                        dtFlag = dvFlag.ToTable()

                                        ImgBut.ID = "imgBut" + i.ToString()
                                        ImgBut.Width = 80
                                        ImgBut.Height = 53.5
                                        ImgBut.CommandArgument = dtFlag.Rows(0).Item("SUPPLIERID").ToString()
                                        ImgBut.ImageUrl = "~/Images/" + dtFlag.Rows(0).Item("IMAGE").ToString() + ""
                                        ImgBut.Attributes.Add("onclick", "window.open('" + dtFlag.Rows(0).Item("IMAGEURL").ToString() + "'); return false;")
                                        AddHandler ImgBut.Click, AddressOf ImgBut_Click
                                        tdInner.Controls.Add(ImgBut)
                                        trInner.Controls.Add(tdInner)

                                End Select
                            Else
                                trInner.Height = 0
                            End If

                        End If
                    Next
                    tblFlagSponsor.Controls.Add(trInner)
                Next
            Else
                ds = objGetData.GetFlagSponsors()
                For i = 0 To ds.Tables(0).Rows.Count + 1
                    trInner = New TableRow
                    For j = 1 To 2
                        tdInner = New TableCell
                        If i < ds.Tables(0).Rows.Count + 1 Then
                            If i > 0 Then
                                Select Case j
                                    Case 1
                                        InnerTdSetting(tdInner, "", "Left")
                                        ImgBut = New ImageButton

                                        ImgBut.ID = "imgBut" + i.ToString()
                                        ImgBut.Width = 80
                                        ImgBut.Height = 53.5
                                        ImgBut.CommandArgument = ds.Tables(0).Rows(i - 1).Item("SUPPLIERID").ToString()
                                        ImgBut.ImageUrl = "~/Images/" + ds.Tables(0).Rows(i - 1).Item("IMAGE").ToString() + ""
                                        ImgBut.Attributes.Add("onclick", "window.open('" + ds.Tables(0).Rows(i - 1).Item("IMAGEURL").ToString() + "'); return false;")
                                        AddHandler ImgBut.Click, AddressOf ImgBut_Click
                                        tdInner.Controls.Add(ImgBut)
                                        trInner.Controls.Add(tdInner)
                                        i = i + 1

                                    Case 2
                                        InnerTdSetting(tdInner, "", "Left")
                                        ImgBut = New ImageButton
                                        ImgBut.ID = "imgBut" + i.ToString()
                                        ImgBut.Width = 80
                                        ImgBut.Height = 53.5
                                        ImgBut.CommandArgument = ds.Tables(0).Rows(i - 1).Item("SUPPLIERID").ToString()
                                        ImgBut.ImageUrl = "~/Images/" + ds.Tables(0).Rows(i - 1).Item("IMAGE").ToString() + ""
                                        ImgBut.Attributes.Add("onclick", "window.open('" + ds.Tables(0).Rows(i - 1).Item("IMAGEURL").ToString() + "'); return false;")
                                        AddHandler ImgBut.Click, AddressOf ImgBut_Click
                                        tdInner.Controls.Add(ImgBut)
                                        trInner.Controls.Add(tdInner)

                                End Select
                            Else
                                trInner.Height = 0
                            End If

                        End If
                    Next
                    tblFlagSponsor.Controls.Add(trInner)
                Next
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    If j = 0 Then
                        FlagSupId = ds.Tables(0).Rows(j).Item("SUPPLIERID").ToString()
                        FlagSupImg = ds.Tables(0).Rows(j).Item("IMAGE").ToString()
                    Else
                        FlagSupId = FlagSupId + "," + ds.Tables(0).Rows(j).Item("SUPPLIERID").ToString()
                        FlagSupImg = FlagSupImg + "," + ds.Tables(0).Rows(j).Item("IMAGE").ToString()
                    End If
                Next
                'objUpIns.InsertFlagTemp(Session.SessionID, FlagSupId, FlagSupImg)
            End If


            For i = 1 To 12
                tr = New TableRow
                td = New TableCell
                hypLink = New HyperLink
                td.CssClass = "SubMenu"
                hypLink.CssClass = "LinkSubMenu"
                Select Case i
                    Case 1
                        hypLink.Text = "INTERACTIVE SERVICES"
                        hypLink.NavigateUrl = "~/InteractiveServices/InteractiveServices.aspx"
                    Case 2
                        hypLink.Text = "<div style='margin-left:10px;'>Structure Assistant</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/SAModule.aspx"
                    Case 3
                        hypLink.Text = "<div style='margin-left:10px;'>Packaging Analysis</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/PackAnalysisService.aspx"
                    Case 4
                        hypLink.Text = "<div style='margin-left:20px;'>Econ1</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/EcoModule1.aspx"
                    Case 5
                        hypLink.Text = "<div style='margin-left:20px;'>Econ2</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/EcoModule2.aspx"
                    Case 6
                        hypLink.Text = "<div style='margin-left:20px;'>Econ3</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/Econ3Module.aspx"

                    Case 7
                        hypLink.Text = "<div style='margin-left:20px;'>Sustain1</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/EnvModule1.aspx"
                    Case 8
                        hypLink.Text = "<div style='margin-left:20px;'>Sustain2</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/EnvModule2.aspx"
                    Case 9
                        hypLink.Text = "<div style='margin-left:20px;'>Sustain3</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/Sustain3Module.aspx"
                    Case 10
                        hypLink.Text = "<div style='margin-left:10px;'>Knowledgebases</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/InteractiveKb.aspx"
                    Case 11
                        hypLink.Text = "<div style='padding-left:10px;'>On-line Research</div>"
                        hypLink.NavigateUrl = "~/InteractiveServices/OnlineResearch.aspx"
                    Case 12
                        hypLink.Text = "<div style='padding-left:10px;'>SEIC</div>"
                        hypLink.NavigateUrl = "~/Pages/Dow/SEICLogin.aspx"

                End Select
                td.Controls.Add(hypLink)
                tr.Controls.Add(td)
                tblLeftMenu.Controls.Add(tr)
            Next

        Catch ex As Exception
            lblError.Text = "Error:Masters_AlliedMaster:GetProductAndServices" + ex.Message.ToString()
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
            Td.Style.Add("font-size", "13px")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ImgBut_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim objUpIns As New StandUpInsData.UpdateInsert()
        Dim FlagSupp As ImageButton = TryCast(sender, ImageButton)
        'objUpIns.InsertLog4(Session("UserId").ToString(), Session("UserName").ToString(), "SupplierMaterials.aspx", Page.Title, "Clicked on Flag for Sponsor: " + FlagSupp.CommandArgument + "", Session("SBACaseId"), "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "Y", FlagSupp.CommandArgument)
        'Response.Redirect("~/Pages/StandAssist/Assumptions/Extrusion.aspx", False)

    End Sub


End Class

