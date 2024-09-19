Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Web.Services
Partial Class AlliedVideo
    Inherits System.Web.UI.Page
    Dim _strCode As String
    Public Property Code() As String
        Get
            Dim ObjCrypto As New CryptoHelper()
            Return ObjCrypto.Decrypt(_strCode)
        End Get
        Set(ByVal value As String)
            _strCode = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Code = Request.QueryString("id")
            hdnCode.Value = Code
            If Code = "vm1" Or Code = "vf1" Then
                lblHeader.Text = "SavvyPack Corporation at a Glance"
                Page.Title = "SavvyPack Corporation at a Glance"
            ElseIf Code = "vm2" Or Code = "vf2" Then
                lblHeader.Text = "SavvyPack<sup>®</sup> Service at a Glance"
                Page.Title = "SavvyPack® Service at a Glance"
            ElseIf Code = "vm3" Or Code = "vf3" Then
                lblHeader.Text = "Benefits for Procurement Professionals"
                Page.Title = "Benefits for Procurement Professionals"
            ElseIf Code = "vm4" Or Code = "vf4" Then
                lblHeader.Text = "Benefits for Sales and Marketing Professionals"
                Page.Title = "Benefits for Sales and Marketing Professionals"
            ElseIf Code = "vm5" Or Code = "vf5" Then
                lblHeader.Text = "SavvyPack<sup>®</sup> Investment Service at a Glance"
                Page.Title = "SavvyPack® Investment Service at a Glance"
            ElseIf Code = "vm6" Or Code = "vf6" Then
                lblHeader.Text = "Benefits for Operations Professionals"
                Page.Title = "Benefits for Operations Professionals"
            ElseIf Code = "vm7" Or Code = "vf7" Then
                lblHeader.Text = "Contract Packager Knowledgebase at a Glance"
                Page.Title = "Contract Packager Knowledgebase at a Glance"
            End If

        Catch ex As Exception

        End Try
    End Sub
    'Company Introduction
    <System.Web.Services.WebMethod()> _
   Public Shared Function Play_VM1(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/AlliedDevelopment/AtaGlance/Allied_ataGlance.mp4"
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function Play_VF1(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/AlliedDevelopment/AtaGlance/Allied_ataGlance.swf"
    End Function
    'SavvyPack® Introduction
    <System.Web.Services.WebMethod()> _
  Public Shared Function Play_VM2(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/SavvyPackAnalyticalService/AtaGlance/SavvyPack_AtaGlance.mp4"
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function Play_VF2(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/SavvyPackAnalyticalService/AtaGlance/SavvyPack_AtaGlance.swf"
    End Function
    'Procurement
    <System.Web.Services.WebMethod()> _
  Public Shared Function Play_VM3(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/SavvyPackAnalyticalService/Procurement/SavvyPack_Procurement_Benefits.mp4"
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function Play_VF3(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/SavvyPackAnalyticalService/Procurement/SavvyPack_Procurement_Benefits.swf"
    End Function
    'Sales and Marketing
    <System.Web.Services.WebMethod()> _
  Public Shared Function Play_VM4(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/SavvyPackAnalyticalService/SalesMarketing/SavvyPack_SalesMarketing_Benefits.mp4"
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function Play_VF4(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/SavvyPackAnalyticalService/SalesMarketing/SavvyPack_SalesMarketing_Benefits.swf"
    End Function

    'Investment Service 
    <System.Web.Services.WebMethod()> _
  Public Shared Function Play_VM5(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/SavvyPackInvestmentService/AtaGlance/SavvyPack_Investment_AtaGlance.mp4"
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function Play_VF5(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/SavvyPackInvestmentService/AtaGlance/SavvyPack_Investment_AtaGlance.swf"
    End Function

    'Operations 
    <System.Web.Services.WebMethod()> _
  Public Shared Function Play_VM6(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/SavvyPackInvestmentService/Operations/SavvyPack_Procurement_Benefits.mp4"
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function Play_VF6(ByVal ss As String) As String
        Return "http://www.savvypack.com/Video/Ar54vC88Uj21/SavvyPackInvestmentService/Operations/SavvyPack_Procurement_Benefits.swf"
    End Function

    'Contract Packaging 
    <System.Web.Services.WebMethod()> _
    Public Shared Function Play_VM7_CHROME(ByVal ss As String) As String
        Return "http://192.168.1.107:5643/Video/Ar54vC88Uj21/ContractPackaging/Captivate/ContractPackagingmp4.htm"
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function Play_VF7(ByVal ss As String) As String
        Return "http://192.168.1.107:5643/Video/Ar54vC88Uj21/ContractPackaging/Captivate/ContractPackaging.htm"
    End Function
    <System.Web.Services.WebMethod()> _
   Public Shared Function Play_VM7(ByVal ss As String) As String
        Return "http://192.168.1.107:5643/Video/Ar54vC88Uj21/ContractPackaging/Captivate/Contract Packager at a Glance.mp4"
    End Function
End Class
