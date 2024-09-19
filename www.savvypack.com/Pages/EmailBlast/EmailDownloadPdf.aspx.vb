Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Diagnostics
Partial Class Pages_EmailBlast_EmailDownloadPdf
    Inherits System.Web.UI.Page

    Protected Sub btn_dwnld_Click(sender As Object, e As System.EventArgs) Handles btn_dwnld.Click
        PDFDwnld(sender, e)
    End Sub
    Protected Sub PDFDwnld1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FILE_NAME As String = "C:\Documents and Settings\All Users\Documents\SavvyPack®_Aluminum_Price _Report_Sample.pdf"
        If System.IO.File.Exists(FILE_NAME) = True Then
            If MsgBox("Do you want to replace file?", MsgBoxStyle.YesNo, "Title") = MsgBoxResult.Yes Then
                IO.File.Copy("E:/Website_Code/15SepFixedCostChanges/Website/www.savvypack.com/Email/SavvyPack®_Aluminum_Price _Report_Sample.pdf", "C:\Documents and Settings\All Users\Documents\SavvyPack®_Aluminum_Price _Report_Sample.pdf", True)
                'My.Computer.Network.DownloadFile("C:/Users/Pratima/Documents/astapkir315.png", "C:\Documents and Settings\All Users\Documents\astapkir315.png")
            End If
        Else
            My.Computer.Network.DownloadFile("E:/Website_Code/15SepFixedCostChanges/Website/www.savvypack.com/Email/SavvyPack®_Aluminum_Price _Report_Sample.pdf", "C:\Documents and Settings\All Users\Documents\SavvyPack®_Aluminum_Price _Report_Sample.pdf")
        End If
        If MsgBox("Do you want to Open file?", MsgBoxStyle.YesNo, "Title") = MsgBoxResult.Yes Then
            If System.IO.File.Exists(FILE_NAME) = True Then
                Diagnostics.Process.Start(FILE_NAME)
            End If
        End If
        Response.Write("<script>window.close();</script>")
    End Sub
    Protected Sub PDFDwnld(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim repDetails As String = ""
        'Dim filename() As String
        Dim DownloadPath As String = String.Empty
        Dim ds As New DataSet
        Dim objGetData As New SavvyGetData.Selectdata()
        Try

            'DownloadPath = "//192.168.3.236/Email/SavvyPack%C2%AE_Aluminum_Price%20_Report_Sample.pdf"
            DownloadPath = "E:/Website_Code/15SepFixedCostChanges/Website/www.savvypack.com/Email/SavvyPack®_Aluminum_Price _Report_Sample.pdf"
            Response.ContentType = "application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment;filename=SavvyPack®_Aluminum_Price _Report_Sample.pdf")
            Response.TransmitFile(DownloadPath)
            Response.End()
            Response.Write("<script>window.close();</script>")

        Catch ex As Exception

        End Try
    End Sub
End Class
