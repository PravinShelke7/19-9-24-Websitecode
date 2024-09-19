Imports System.Data
Partial Class Universal_loginN_Pages_PProdLicenseAgr
    Inherits System.Web.UI.Page
    Dim _strSchema As String
    Dim _strSerID As String
    Public Property Schema() As String
        Get
            Return _strSchema
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strSchema = value
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
        Dim obj As New CryptoHelper
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Allied Development Corp."
            ServID = Request.QueryString("ServID").ToString()
            Schema = Request.QueryString("Schema").ToString()
            'If Not IsPostBack Then
            '    ServID = Request.QueryString("ServID").ToString()
            '    If ServID.ToString() <> "" Then
            '        hidTId.Value = obj.Decrypt(ServID).Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            '    Else
            '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "LicenAgreemnt", "javascript:alert('Something went wrong! Please try again later.'); window.close();", True)
            '    End If
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Try
            Dim objUpdateData As New LoginUpdateData.Selectdata
            Dim password As String
            Dim userName As String
            userName = Session("USERID").ToString()
            password = Session("Password").ToString()
            objUpdateData.InsertLicense(userName, password, "PACKPROD")
            Response.Redirect("ModuleRedirect.aspx?ServID=" + ServID + "&Schema=" + Schema + "", False)
            'FindCntrServc()
            'Response.Redirect("UniversalM.aspx")
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClsTab", "CloseTab();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnAccept_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnDecline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDecline.Click
        Try
            'Response.Redirect("UniversalM.aspx")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseWindow", "javascript:window.close();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnDecline_Click:" + ex.Message.ToString()
        End Try
    End Sub

    'Protected Sub FindCntrServc()
    '    Dim objGetData As New LoginGetData.Selectdata()
    '    Dim ds As New DataSet()
    '    Try
    '        If hidTId.Value = "21" Then
    '            Session("TID") = hidTId.Value
    '            ModuleKnowBaseRedirection("ContractConnectionString", "CONTR")
    '        ElseIf hidTId.Value = "20" Then
    '            Session("TID") = hidTId.Value
    '            ModuleKnowBaseRedirection("PackProdConnectionString", "PACKPROD")
    '        Else
    '            Response.Redirect("~/Pages/Contract/security.asp?ID=" + Session("ID").ToString() + "&C1=" + hidTId.Value + "", False)
    '        End If
    '        'Response.Redirect("../../Contract/security.asp?ID=" + Session("ID").ToString() + "&C1=" + ddlKBase.SelectedItem.Value.ToString(), True)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub ModuleKnowBaseRedirection(ByVal strCon As String, ByVal modName As String)
    '    Dim objGetData As New LoginGetData.Selectdata()
    '    Dim objUpdateData As New LoginUpdateData.Selectdata()
    '    Dim obj As New CryptoHelper
    '    Dim ds, dsUser, dsCont, dsLic As New DataSet()
    '    Dim dsLicenseUser As New DataSet()
    '    Try
    '        'Get Max Count of License
    '        dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), strCon)
    '        Session("MaxLicKCount") = dsLic.Tables(0).Rows(0).Item("CONTRMAXCOUNT")
    '        dsUser = objGetData.GetInuseDetailsByUser(Session("UserId").ToString(), strCon)
    '        If CInt(dsUser.Tables(0).Rows(0).Item("count")) = 0 Then
    '            ds = objGetData.GetInuseDetails(Session("UserId").ToString(), strCon)
    '            If CInt(ds.Tables(0).Rows(0).Item("count")) >= CInt(Session("MaxLicKCount")) Then
    '                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
    '            Else
    '                dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), strCon, modName)
    '                If dsLicenseUser.Tables(0).Rows.Count > 0 Then
    '                    'Inserting Data in Inuse table

    '                    If modName = "CONTR" Then
    '                        objUpdateData.InuseLoginInsertContract(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
    '                        dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
    '                        If dsCont.Tables(0).Rows.Count = 0 Then
    '                            objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
    '                        End If
    '                    ElseIf modName = "PACKPROD" Then
    '                        objUpdateData.InuseLoginInsertPackProd(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), Request.ServerVariables("REMOTE_ADDR").ToString(), Request.ServerVariables("HTTP_ACCEPT_LANGUAGE").ToString(), Request.ServerVariables("HTTP_USER_AGENT").ToString(), strCon)
    '                        dsCont = objGetData.GetSelectionData(Session("USERID").ToString(), Session("Password").ToString(), strCon)
    '                        If dsCont.Tables(0).Rows.Count = 0 Then
    '                            objUpdateData.InsertSelection(Session("USERID").ToString(), Session("Password").ToString(), strCon)
    '                        End If
    '                    End If
    '                    Response.Redirect("~/Session.aspx?Moduls=" + modName + "&ID=" + "", False)
    '                Else
    '                    If modName = "CONTR" Then
    '                        Response.Redirect("~/Universal_loginN/Pages/ContLicenseAgr.aspx?ServID=", False)
    '                    ElseIf modName = "PACKPROD" Then
    '                        Response.Redirect("~/Universal_loginN/Pages/PProdLicenseAgr.aspx?ServID=", False)
    '                    Else
    '                        Response.Redirect("~/Universal_loginN/Pages/EconLicenseAgreement.aspx?ServID=", False)
    '                    End If
    '                End If
    '            End If
    '        Else
    '            Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt(strCon).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
 
End Class
