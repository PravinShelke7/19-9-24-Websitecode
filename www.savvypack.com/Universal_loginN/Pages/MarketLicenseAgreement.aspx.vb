Imports System.Data
Partial Class Universal_loginN_Pages_MarketLicenseAgreement
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
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
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
            objUpdateData.InsertLicense(userName, password, "Market1")
            Response.Redirect("ModuleRedirect.aspx?ServID=" + ServID + "&Schema=" + Schema + "", False)
            'Mrkt1Servc()
        Catch ex As Exception
            lblError.Text = "Error:btnAccept_Click:" + ex.Message.ToString()
        End Try

    End Sub

    Protected Sub btnDecline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDecline.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseWindow", "javascript:window.close();", True)
        Catch ex As Exception
            lblError.Text = "Error:btnDecline_Click:" + ex.Message.ToString()
        End Try

    End Sub

    'Protected Sub Mrkt1Servc()
    '    Dim objGetData As New LoginGetData.Selectdata()
    '    Dim objUpdateData As New LoginUpdateData.Selectdata()
    '    Dim obj As New CryptoHelper
    '    Dim ds, dsLic, dsUser As New DataSet()
    '    Dim dsLicenseUser As New DataSet()
    '    Try
    '        dsLic = objGetData.GetMaxCountByLic(Session("UserId").ToString(), "Market1ConnectionString")
    '        ds = objGetData.GetInuseDetailsByUserMKT(Session("UserId").ToString(), "Market1ConnectionString")
    '        Session("MaxLicCount") = dsLic.Tables(0).Rows(0).Item("MAXCOUNT")
    '        If CInt(ds.Tables(0).Rows(0).Item("count")) = 0 Then
    '            'dsUser = objGetData.GetInuseDetailsByLic(Session("UserId").ToString())
    '            dsLicenseUser = objGetData.ValidateLicenseUser(Session("USERID").ToString(), "Market1ConnectionString", "Market1")
    '            If dsLicenseUser.Tables(0).Rows.Count > 0 Then
    '                'Inserting Data in Inuse table
    '                objUpdateData.InuseLoginInsert(Session.SessionID, Session("USERID").ToString(), Session("LicenseNo").ToString().Replace("'", "''"), "Market1ConnectionString")
    '                Session("M1ServiceID") = hidTId.Value
    '                Session("TID") = hidTId.Value
    '                Response.Redirect("~/Pages/Market1Sub/Default.aspx", False)
    '            Else
    '                Response.Redirect("~/Universal_loginN/Pages/MarketLicenseAgreement.aspx?ServID=", False)
    '            End If
    '        Else
    '            Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE115").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "&Schema=" + obj.Encrypt("Market1ConnectionString").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!"))
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

End Class
