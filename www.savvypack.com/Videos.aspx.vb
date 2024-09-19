Imports System.Data
Imports System.Data.OleDb
Partial Class Videos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Getting Guid
            If Not IsPostBack Then
                ViewState("GUID") = Guid.NewGuid().ToString()
            End If



            Dim ObjCrypto As New CryptoHelper()
            'Company Introduction
            lnkVidM1.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vm1") + "&Val=" + ViewState("GUID").ToString() + "');")
            lnkVidF1.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vf1") + "&Val=" + ViewState("GUID").ToString() + "');")
            'SavvyPack® Introduction
            lnkVidM2.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vm2") + "&Val=" + ViewState("GUID").ToString() + "');")
            lnkVidF2.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vf2") + "&Val=" + ViewState("GUID").ToString() + "');")
            'Procurement 
            lnkVidM3.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vm3") + "&Val=" + ViewState("GUID").ToString() + "');")
            lnkVidF3.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vf3") + "&Val=" + ViewState("GUID").ToString() + "');")
            'Sales and Marketing
            lnkVidM4.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vm4") + "&Val=" + ViewState("GUID").ToString() + "');")
            lnkVidF4.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vf4") + "&Val=" + ViewState("GUID").ToString() + "');")
            'Investment  Service
            lnkVidM5.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vm5") + "&Val=" + ViewState("GUID").ToString() + "');")
            lnkVidF5.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vf5") + "&Val=" + ViewState("GUID").ToString() + "');")
            'Operations
            lnkVidM6.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vm6") + "&Val=" + ViewState("GUID").ToString() + "');")
            lnkVidF6.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vf6") + "&Val=" + ViewState("GUID").ToString() + "');")
            'Contract Packaging
            lnkVidConM1.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vm7") + "&Val=" + ViewState("GUID").ToString() + "');")
            lnkVidConF1.Attributes.Add("onclick", "ShowPopWindow('AlliedVideo.aspx?id=" + ObjCrypto.Encrypt("vf7") + "&Val=" + ViewState("GUID").ToString() + "');")

        Catch ex As Exception

        End Try
    End Sub
End Class
