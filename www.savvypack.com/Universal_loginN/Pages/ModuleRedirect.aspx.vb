Imports System.Data
Partial Class Universal_loginN_Pages_ModuleRedirect
    Inherits System.Web.UI.Page
    Dim _strSchema As String
    Dim _strSerID As String

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
            _strSerID = obj.Decrypt(value)
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ServID = Request.QueryString("ServID").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            Schema = Request.QueryString("Schema").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            If Not IsPostBack Then
                If Schema = "ReportConnectionString" Or Schema = "ContractConnectionString" Or Schema = "PackProdConnectionString" Then
                    Contract_PackPro()
                ElseIf Schema = "Market1ConnectionString" Then
                    MarketModule()
                Else
                    OtherModules()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

#Region "Contract_PackPeod_ETC"

    Protected Sub Contract_PackPro()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If ServID = "21" Then
                Session("TID") = ServID
                ModuleKnowBaseRedirection("ContractConnectionString", "CONTR")
            ElseIf ServID = "20" Then
                Session("TID") = ServID
                ModuleKnowBaseRedirection("PackProdConnectionString", "PACKPROD")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ModuleKnowBaseRedirection(ByVal strCon As String, ByVal modName As String)
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds, dsUser, dsCont, dsLic As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            'Get Max Count of License
            dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), strCon)
            Session("MaxLicKCount") = dsLic.Tables(0).Rows(0).Item("CONTRMAXCOUNT")

            'Old Code: Inuse table check Start
            'dsUser = objGetData.GetInuseDetailsByUser(Session("UserId").ToString(), strCon)

            'If CInt(dsUser.Tables(0).Rows(0).Item("count")) = 0 Then
            '    ds = objGetData.GetInuseDetails(Session("UserId").ToString(), strCon)
            '    If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicKCount")) Then
            '        Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(ServID).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
            '    Else
            '        'Inserting Data in Inuse table
            '        If modName = "CONTR" Then
            '            objUpdateData.InuseLoginInsertContract(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
            '            dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '            If dsCont.Tables(0).Rows.Count = 0 Then
            '                objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '            End If
            '        ElseIf modName = "PACKPROD" Then
            '            objUpdateData.InuseLoginInsertPackProd(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
            '            dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '            If dsCont.Tables(0).Rows.Count = 0 Then
            '                objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '            End If
            '        End If
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('/Session.aspx?Moduls=" + modName + "&ID=" + "','_blank'); window.close();", True)
            '    End If
            'Else
            '    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(ServID).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
            'End If
            'Old Code: Inuse table check End

            ds = objGetData.GetInuseDetails(Session("UserId").ToString(), strCon)
            If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicKCount")) Then
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(ServID).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
            Else
                'Delete data from Inuse Tbale
                objUpdateData.DelInUseEntry(Session("USERID").ToString(), modName)

                'Inserting Data in Inuse table
                If modName = "CONTR" Then
                    objUpdateData.InuseLoginInsertContract(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
                    dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                    If dsCont.Tables(0).Rows.Count = 0 Then
                        objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                    End If
                ElseIf modName = "PACKPROD" Then
                    objUpdateData.InuseLoginInsertPackProd(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
                    dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                    If dsCont.Tables(0).Rows.Count = 0 Then
                        objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
                    End If
                End If
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('/Session.aspx?Moduls=" + modName + "&ID=" + "','_blank'); window.close();", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Market"

    Protected Sub MarketModule()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds, dsLic, dsUser As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            'Old Code: Inuse table check Start
            'dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), "Market1ConnectionString")
            'ds = objGetData.GetInuseDetailsByUserMKT(Session("UserId").ToString(), "Market1ConnectionString")
            'Session("MaxLicCount") = dsLic.Tables(0).Rows(0).Item("MAXCOUNT")
            'If CInt(ds.Tables(0).Rows(0).Item("count")) = 0 Then
            '    'Inserting Data in Inuse table
            '    objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), "Market1ConnectionString")
            '    Session("M1ServiceID") = ServID
            '    Session("TID") = ServID
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('/Pages/Market1Sub/Default.aspx'); window.close();", True)
            'Else
            '    Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(ServID).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
            'End If
            'Old Code: Inuse table check End

            dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), "Market1ConnectionString")
            ds = objGetData.GetInuseDetailsByUserMKT(Session("UserId").ToString(), "Market1ConnectionString")
            Session("MaxLicMCount") = dsLic.Tables(0).Rows(0).Item("MRKTMAXCOUNT")

            If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicMCount")) Then
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(ServID).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
            Else
                'Delete data from Inuse Tbale
                objUpdateData.DelInUseEntry(Session("USERID").ToString(), "M1")

                'Inserting Data in Inuse table
                objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), "Market1ConnectionString")
                Session("M1ServiceID") = ServID
                Session("TID") = ServID
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('/Pages/Market1Sub/Default.aspx'); window.close();", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Other Services"

    Protected Sub OtherModules()
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            If ServID = "1" Then
                Session("TID") = ServID
                Session("Service") = Nothing
                ModuleRedirection("EconConnectionString", "E1")

            ElseIf ServID = "2" Then
                Session("TID") = ServID
                ModuleRedirection("Econ2ConnectionString", "E2")

            ElseIf ServID = "3" Then
                Session("TID") = ServID
                ModuleRedirection("Econ3ConnectionString", "E3")

            ElseIf ServID = "4" Then
                Session("TID") = ServID

            ElseIf ServID = "5" Then
                Session("Service") = Nothing
                Session("TID") = ServID
                ModuleRedirection("Sustain1ConnectionString", "S1")

            ElseIf ServID = "6" Then
                Session("TID") = ServID
                ModuleRedirection("Sustain2ConnectionString", "S2")

            ElseIf ServID = "7" Then
                Session("TID") = ServID
                ModuleRedirection("Echem1ConnectionString", "EC1")

            ElseIf ServID = "8" Then
                Session("TID") = ServID
                ModuleRedirection("Schem1ConnectionString", "SC1")

            ElseIf ServID = "9" Then
                Session("TID") = ServID
                ModuleRedirection("Sustain3ConnectionString", "S3")

            ElseIf ServID = "10" Then
                Session("TID") = ServID
                ModuleRedirection("Sustain4ConnectionString", "S4")

            ElseIf ServID = "11" Then
                Session("TID") = ServID
                ModuleRedirection("Econ4ConnectionString", "E4")

            ElseIf ServID = "12" Then
                Session("TID") = ServID
                ModuleRedirection("Market1ConnectionString", "M1")

            ElseIf ServID = "23" Then
                Session("TID") = ServID
                ModuleRedirection("SpecConnectionString", "SPEC")
            ElseIf ServID = "18" Then
                Session("TID") = ServID
                ModuleRedirection("DistributionConnectionString", "DIST")
            ElseIf ServID = "19" Then
                Session("TID") = ServID
                ModuleRedirection("RetailConnectionString", "RETL")
            ElseIf ServID = "22" Then
                Session("TID") = ServID
                ModuleRedirection("SDistributionConnectionString", "SDIST")
                'Change for Value Chain
            ElseIf ServID = "24" Then
                Session("TID") = ServID
                ModuleRedirection("VChainConnectionString", "VChain")
            ElseIf ServID = "26" Then
                Session("TID") = ServID
                Session("Service") = "COMP"
                ModuleRedirection("EconConnectionString", "COMP")
            ElseIf ServID = "27" Then
                Session("TID") = ServID
                Session("Service") = "COMPS1"
                ModuleRedirection("Sustain1ConnectionString", "COMPS1")
            ElseIf ServID = "28" Then
                Session("TID") = ServID
                Session("Service") = "SBASSIST"
                ModuleRedirection("SBAConnectionString", "StandAssist")
            ElseIf ServID = "29" Then
                Session("TID") = ServID
                ModuleRedirection("MedEcon1ConnectionString", "MEDECON1")
            ElseIf ServID = "30" Then
                Session("TID") = ServID
                ModuleRedirection("MedEcon2ConnectionString", "MEDECON2")
            ElseIf ServID = "31" Then
                Session("TID") = ServID
                ModuleRedirection("MedSustain1ConnectionString", "MEDSUSTAIN1")
            ElseIf ServID = "32" Then
                Session("TID") = ServID
                ModuleRedirection("MedSustain2ConnectionString", "MEDSUSTAIN2")
            ElseIf ServID = "35" Then
                Session("TID") = ServID
                ModuleRedirection("MoldE1ConnectionString", "MOLDE1")
            ElseIf ServID = "36" Then
                Session("TID") = ServID
                ModuleRedirection("MoldE2ConnectionString", "MOLDE2")
            ElseIf ServID = "37" Then
                Session("TID") = ServID
                ModuleRedirection("MoldS1ConnectionString", "MOLDS1")
            ElseIf ServID = "38" Then
                Session("TID") = ServID
                ModuleRedirection("MoldS2ConnectionString", "MOLDS2")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ModuleRedirection(ByVal strCon As String, ByVal modName As String)
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim obj As New CryptoHelper
        Dim ds, dsUser, dsCont, dsLic, dsId As New DataSet()
        Dim dsLicenseUser As New DataSet()
        Try
            'Get Max Count of License
            dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), strCon)
            Session("MaxLicCount") = dsLic.Tables(0).Rows(0).Item("MAXCOUNT")

            'Old Code: Inuse table check Start
            'If modName = "COMP" Or modName = "COMPS1" Then
            '    dsUser = objGetData.GetInuseDetailsByCompUser(Session("UserId").ToString(), strCon)
            'Else
            '    dsUser = objGetData.GetInuseDetailsByUser(Session("UserId").ToString(), strCon)
            'End If

            'If CInt(dsUser.Tables(0).Rows(0).Item("count")) = 0 Then
            '    ds = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
            '    If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicCount")) Then
            '        Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(ServID).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
            '    Else
            '        If modName = "CONTR" Then
            '            objUpdateData.InuseLoginInsertContract(Session.SessionID, Session("UserId").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
            '            dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '            If dsCont.Tables(0).Rows.Count = 0 Then
            '                objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '            End If
            '        ElseIf modName = "PACKPROD" Then
            '            objUpdateData.InuseLoginInsertPackProd(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
            '            dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '            If dsCont.Tables(0).Rows.Count = 0 Then
            '                objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
            '            End If
            '        Else
            '            If modName = "COMP" Or modName = "COMPS1" Then
            '                objUpdateData.InuseCompLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
            '            Else
            '                objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
            '            End If
            '        End If
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('/Session.aspx?Moduls=" + modName + "&ID=" + "','_blank'); window.close();", True)
            '    End If
            'Else
            '    dsId = objGetData.ValidateID(Session("UserName"))
            '    If dsId.Tables(0).Rows(0).Item("ISINTERNALUSR").ToString() = "Y" Then
            '        ds = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
            '        If CInt(ds.Tables(0).Rows(0).Item("count")) > CInt(Session("MaxLicCount")) Then
            '            Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(ServID).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
            '        Else
            '            If modName = "COMP" Or modName = "COMPS1" Then
            '                objUpdateData.InuseCompLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
            '            Else
            '                objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
            '            End If
            '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('/Session.aspx?Moduls=" + modName + "&ID=" + "','_blank'); window.close();", True)
            '        End If
            '    Else
            '        Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(ServID).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
            '    End If
            'End If
            'Old Code: Inuse table check End

            ds = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
            If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicCount")) Then
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&ServID=" + obj.Encrypt(ServID).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"), False)
            Else
                'Delete data from Inuse Tbale
                objUpdateData.DelInUseEntry(Session("USERID").ToString(), modName)

                'Inserting Data in Inuse table
                If modName = "COMP" Or modName = "COMPS1" Then
                    objUpdateData.InuseCompLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
                Else
                    objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), strCon)
                End If
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "UserServ", "javascript:window.open('/Session.aspx?Moduls=" + modName + "&ID=" + "','_blank'); window.close();", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
